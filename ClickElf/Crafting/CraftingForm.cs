using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf.Crafting
{
    public partial class CraftingForm : Form
    {
        private Tuple<SettingsControlSet, SettingsControlSet>[] doubleSetsTuple;

        private int m1Time = 0, m2Time = 0, m3Time = 0;
        private string startPos = "", m1Pos = "", m2Pos = "", m3Pos = "";
        public CraftingForm()
        {
            InitializeComponent();
            setSets();
            txtMacroBox.LostFocus += TxtMacroBox_LostFocus;

        }

        private void setSets()
        {
            doubleSetsTuple = new Tuple<SettingsControlSet, SettingsControlSet>[3];
            doubleSetsTuple[0] = new Tuple<SettingsControlSet, SettingsControlSet>
                (new SettingsControlSet(0x01, lblM1Pos, txtM1Pos),
                 new SettingsControlSet(0x11, lblM1WaitTime, txtM1WaitTime));
            doubleSetsTuple[1] = new Tuple<SettingsControlSet, SettingsControlSet>
                (new SettingsControlSet(0x02, lblM2Pos, txtM2Pos),
                 new SettingsControlSet(0x12, lblM2WaitTime, txtM2WaitTime));
            doubleSetsTuple[2] = new Tuple<SettingsControlSet, SettingsControlSet>
                (new SettingsControlSet(0x03, lblM3Pos, txtM3Pos),
                 new SettingsControlSet(0x13, lblM3WaitTime, txtM3WaitTime));
            doubleSetsTuple[0].Item1.lbl.Visible = false; doubleSetsTuple[0].Item1.tBox.Visible = false; doubleSetsTuple[0].Item2.lbl.Visible = false; doubleSetsTuple[0].Item2.tBox.Visible = false;
            doubleSetsTuple[1].Item1.lbl.Visible = false; doubleSetsTuple[1].Item1.tBox.Visible = false; doubleSetsTuple[1].Item2.lbl.Visible = false; doubleSetsTuple[1].Item2.tBox.Visible = false;
            doubleSetsTuple[2].Item1.lbl.Visible = false; doubleSetsTuple[2].Item1.tBox.Visible = false; doubleSetsTuple[2].Item2.lbl.Visible = false; doubleSetsTuple[2].Item2.tBox.Visible = false;
        }

        private void TxtMacroBox_LostFocus(object sender, EventArgs e)
        {
            if (txtMacroBox.Text == "") return;
            txtMacroBox.Text = txtMacroBox.Text.TrimEnd();
            string[] macroLines = txtMacroBox.Text.Replace("\r\n", "\n").Split('\n');
            var result = macroLines
                .Select((value, index) => new { value, index })
                .GroupBy(x => macroLines.Take(x.index + 1).Count(y => y == ""))
                .Select(g => g.Select(x => x.value).ToArray())
                .ToArray();
            if (result.Length > 3) { MessageBox.Show("目前最多支持3个宏！", "Crafting Helper", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMacroBox.Focus(); return; }
            string pattern = @"<wait\.(\d+)>";
            if (result.Length >= 1)
            {
                doubleSetsTuple[0].Item1.lbl.Visible = true; doubleSetsTuple[0].Item1.tBox.Visible = true; doubleSetsTuple[0].Item2.lbl.Visible = true; doubleSetsTuple[0].Item2.tBox.Visible = true;
                m1Time = 0;
                foreach (var s in result[0])
                {
                    if (!s.StartsWith("/ac")) continue;
                    Match m = Regex.Match(s, pattern);
                    int num = int.Parse(m.Groups[1].Value);
                    m1Time += num;
                }
                txtM1WaitTime.Text = m1Time.ToString();
            }
            if (result.Length >= 2)
            {
                doubleSetsTuple[1].Item1.lbl.Visible = true; doubleSetsTuple[1].Item1.tBox.Visible = true; doubleSetsTuple[1].Item2.lbl.Visible = true; doubleSetsTuple[1].Item2.tBox.Visible = true;
                m2Time = 0;
                foreach (var s in result[1])
                {
                    if (!s.StartsWith("/ac")) continue;
                    Match m = Regex.Match(s, pattern);
                    int num = int.Parse(m.Groups[1].Value);
                    m2Time += num;
                }
                txtM2WaitTime.Text = m2Time.ToString();
            }
            if (result.Length >= 3)
            {
                doubleSetsTuple[2].Item1.lbl.Visible = true; doubleSetsTuple[2].Item1.tBox.Visible = true; doubleSetsTuple[2].Item2.lbl.Visible = true; doubleSetsTuple[2].Item2.tBox.Visible = true;
                m3Time = 0;
                foreach (var s in result[2])
                {
                    if (!s.StartsWith("/ac")) continue;
                    Match m = Regex.Match(s, pattern);
                    int num = int.Parse(m.Groups[1].Value);
                    m3Time += num;
                }
                txtM3WaitTime.Text = m3Time.ToString();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            new CraftingFormSettings().Show();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                startPos = CraftingData.FormatPointString(txtStartPos.Text, 1);
                if (txtM1Pos.Visible) m1Pos = CraftingData.FormatPointString(txtM1Pos.Text, 1);
                if (txtM2Pos.Visible) m2Pos = CraftingData.FormatPointString(txtM2Pos.Text, 1);
                if (txtM3Pos.Visible) m3Pos = CraftingData.FormatPointString(txtM3Pos.Text, 1);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("有参数未输入或格式不正确！", "Crafting Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> scripts = new List<string>();
            if (txtAutoDelay.Text != "" && txtAutoDelay.Text != "0")
                scripts.Add($"#AutoDelay {txtAutoDelay.Text}");
            scripts.Add($"mv {startPos}; mc l");
            scripts.Add("w 1500");
            if (txtM1Pos.Visible) scripts.Add($"mv {m1Pos}; mc l; w {m1Time * 1000}");
            if (txtM2Pos.Visible) scripts.Add($"mv {m2Pos}; mc l; w {m2Time * 1000}");
            if (txtM3Pos.Visible) scripts.Add($"mv {m3Pos}; mc l; w {m3Time * 1000}");
            string scriptText = "";
            foreach (var s in scripts)
                scriptText += s + "\r\n";
            var result = MessageBox.Show($"{scriptText}", "Script preview", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string fileName = $"script_{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}.txt";
                File.WriteAllText(".\\" + fileName, scriptText, Encoding.UTF8);
                MessageBox.Show($"脚本已生成为: {fileName}");
            }
        }
    }
}
