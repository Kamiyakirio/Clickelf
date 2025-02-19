using ClickElf.Key;
using ClickElf.Mouse;
using ClickElf.Screen;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf
{
    public partial class FormImf : Form
    {
        [DllImport("user32.dll")]
        private static extern void SetForegroundWindow(IntPtr hWnd);
        private MouseHandler mouseHandler;
        private KeyHandler keyHandler;
        public FormImf()
        {
            InitializeComponent();
            mouseHandler = new MouseHandler();
            //mouseTracker = new MouseTracker();
            mouseHandler.mouseTracker.MouseMoved += OnMouseMoved;
            listBox1.Items.Clear();

            keyHandler = new KeyHandler();
            keyHandler.keyHook.KeyPressed += (key, status) =>
            {
                lblKey.Text = $"Pressed key: {key}";
                if (key == Keys.LShiftKey) lblShift.Text = status ? $"Shift Down" : "Shift Up";
                else if (key == Keys.LControlKey) lblCtrl.Text = status ? "Ctrl Down" : "Ctrl Up";
                else if (key == Keys.LMenu) lblAlt.Text = status ? "Alt Down" : "Alt Up";
            };

            mouseHandler.mouseTracker.MousePress += (point, status, btn) =>
            {
                if (btn == 0) {/* Console.WriteLine($"Left button {(status ? "Down" : "Up")}"); */}
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(123);
            textBox1.Focus();
            lblAlt.Text = "";
            lblCtrl.Text = "";
            lblShift.Text = "";
            lblColor.Text = "";
            // textBox1.LostFocus += (psender, pe) => { this.ActiveControl = textBox1; };
            ProcessDelegates();
        }

        private void ProcessDelegates()
        {
            // this.CanFocus = false;
            button3.Click += async (sender, e) =>
            {
                await Task.Delay(500);
                await keyHandler.SendStringWithEscapesAsync(textBox3.Text);
            };
        }
        private void OnMouseMoved(int x, int y)
        {
            lblMouse.Text = $"x:{x}, y:{y}";
            Color _color = Screenshot.GetColor(new Point(x, y));
            lblColor.Text = _color.ToString();
            colorBox.BackColor = _color;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(lblMouse.Text);
            // await keyHandler.SendKeyAwait((ushort)Keys.A,500);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                button2.PerformClick();
                // keyHandler.SendKey((ushort)Keys.A);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x, y;
            if (!int.TryParse(textBox1.Text, out x) || !int.TryParse(textBox2.Text, out y))
            {
                MessageBox.Show("Invalid Input!", "foo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mouseHandler.MouseMove(x, y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Screenshot.Shot(new Point(114, 514), new Point(514, 810));
            Screenshot.GetColor(new Point(114, 514));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Crafting.CraftingForm().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OCR.OCRBase.Do();
        }
    }
}
