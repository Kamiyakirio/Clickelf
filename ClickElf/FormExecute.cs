using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickElf.Interpretor;
using ClickElf.Key;
using ClickElf.Mouse;

namespace ClickElf
{
    public partial class FormExecute : Form
    {
        private OpenFileDialog openFileDialog;
        private string filePath = "";
        private Interpretor.Interpretor interpretor;
        private KeyHandler keyHandler;
        private MouseHandler mouseHandler;
        private FormImf formImf;

        private CancellationTokenSource _cts;

        private Dictionary<uint, bool> keyStatus;

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_SIZE = 0xF000;
            const int SC_MAXIMIZE = 0xF030;
            if (m.Msg == WM_SYSCOMMAND && ((m.WParam.ToInt32() & 0xFFF0) == SC_SIZE) || (m.WParam.ToInt32() & 0xFFF0) == SC_MAXIMIZE)
            {
                return; // 拦截调整大小消息，阻止窗口变大或缩小
            }
            base.WndProc(ref m);
        }
        public FormExecute()
        {
            InitializeComponent();
            InitializeDialog();
            formImf = new FormImf();
            formImf.FormClosing += (sender, e) => { formImf.Hide(); e.Cancel = true; };
            btnStop.Enabled = false;
            keyHandler = new KeyHandler();
            mouseHandler = new MouseHandler();
            keyStatus = new Dictionary<uint, bool>();
            keyHandler.keyHook.KeyPressed += (key, status) =>
            {
                keyStatus[(uint)key] = status;
                try
                {
                    if (keyStatus[(uint)Keys.LControlKey] && keyStatus[(uint)Keys.C]) { btnStop.PerformClick(); }
                }
                catch (Exception ex) { /*Console.WriteLine($"{ex.Message}");*/ }
            };
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InitializeDialog()
        {
            openFileDialog = new OpenFileDialog();
            //openFileDialog.ShowDialog(this);
            lblFileName.Text = "";
            textTimes.Text = "1";
        }

        private void FormExecute_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // openFileDialog.ShowDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                lblFileName.Text = filePath;
            }
            else { filePath = ""; lblFileName.Text = "未选择文件"; return; }
            interpretor = new Interpretor.Interpretor(filePath, keyHandler, mouseHandler);
            foreach (var item in interpretor.GetLines())
            {
                Console.WriteLine(item);
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (filePath == "")
            {
                MessageBox.Show("没有选择脚本文件！", "foo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int times;
            if (!int.TryParse(textTimes.Text, out times))
            {
                MessageBox.Show("输入错误！", "foo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            interpretor = new Interpretor.Interpretor(filePath, keyHandler, mouseHandler);

            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "执行中";
            btnStop.Enabled = true;
            for (int i = 0; i < times; ++i)
            {
                Task task = Task.Run(() => Execute(interpretor, token), token);
                await task;
                if (token.IsCancellationRequested) break;
            }
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = token.IsCancellationRequested ? "已取消" : "已完成";
            btnStop.Enabled = false;
        }

        private void Execute(Interpretor.Interpretor interpretor, CancellationToken token)
        {
            if (token.IsCancellationRequested) return;
            interpretor.Execute();
            if (token.IsCancellationRequested) return;
        }

        private void btnSwitchForm_Click(object sender, EventArgs e)
        {
            formImf.Show();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnStop.Enabled = false;
        }
    }
}
