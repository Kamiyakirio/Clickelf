using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf.Crafting
{

    public partial class CraftingFormSettings : Form
    {
        private Dictionary<int, SettingsControlSet>[] controlDicts;
        private Dictionary<int, string> skillPoint;
        private const string settingFile = "points.json";

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

        public CraftingFormSettings()
        {
            skillPoint = LoadSetting();
            InitializeComponent();
            ConstructComponent();
            ProcessControlText();
            this.FormClosing += (sender, e) =>
            {
                SaveSettings();
            };
        }

        private void ConstructComponent()
        {
            controlDicts = new Dictionary<int, SettingsControlSet>[5];

            foreach (var control in Controls)
            {
                if (control is GroupBox groupBox)
                {
                    ProcessGroupBox(groupBox);
                }
            }
        }

        private void ProcessGroupBox(GroupBox groupBox)
        {
            foreach (var control in groupBox.Controls)
            {
                if (control is TableLayoutPanel tableLayoutPanel)
                {
                    ProcessTableLayoutPanel(tableLayoutPanel);
                }
            }
        }

        private void ProcessTableLayoutPanel(TableLayoutPanel tableLayoutPanel)
        {
            foreach (var control in tableLayoutPanel.Controls)
            {
                string itemString = control.ToString();
                if (itemString.Contains("Text:"))
                {
                    ProcessControl((Control)control, itemString);
                }
            }
        }

        private void ProcessControl(Control control, string itemString)
        {
            int index = itemString.IndexOf("Text:");
            string noString = itemString.Substring(index + 6);
            int no = Convert.ToInt32(noString, 16);
            int controlDictsIndex = ((no & 0x0F00) >> 8) - 1;

            if (controlDicts[controlDictsIndex] == null)
            {
                controlDicts[controlDictsIndex] = new Dictionary<int, SettingsControlSet>();
            }

            if (!controlDicts[controlDictsIndex].ContainsKey(no))
            {
                controlDicts[controlDictsIndex][no] = new SettingsControlSet { id = no };
            }

            var settingsControlSet = controlDicts[controlDictsIndex][no];

            if (control is Label label)
            {
                settingsControlSet.lbl = label;
            }
            else if (control is TextBox textBox)
            {
                settingsControlSet.tBox = textBox;
            }
        }

        private void ProcessControlText()
        {
            var skillsDict = CraftingData.getSkills();
            foreach (var dict in controlDicts)
            {
                if (dict != null)
                {
                    foreach (var item in dict)
                    {
                        item.Value.tBox.Text = skillPoint[item.Key] == "(0, 0)" ? "" : skillPoint[item.Key];
                        item.Value.lbl.Text = skillsDict[item.Value.id.ToString()][0];
                        item.Value.tBox.TabIndex = item.Key - 0x0100;
                        item.Value.tBox.LostFocus += (sender, e) =>
                        {
                            try
                            {
                                if (item.Value.tBox.Text == "") { skillPoint[item.Key] = CraftingData.FormatPointString("0,0"); return; }
                                string formatted = CraftingData.FormatPointString(item.Value.tBox.Text);
                                skillPoint[item.Key] = formatted;
                            }
                            catch (ArgumentException)
                            {
                                MessageBox.Show("不合法的输入");
                                item.Value.tBox.Text = "";
                            }
                        };
                    }
                }
            }
        }

        private Dictionary<int, string> LoadSetting()
        {
            if (!File.Exists(settingFile))
            {
                File.Create(settingFile).Dispose();
                var skillDict = CraftingData.getSkills();
                if (skillPoint == null) skillPoint = new Dictionary<int, string>();
                skillPoint.Clear();
                foreach (var item in skillDict)
                {
                    skillPoint[Convert.ToInt32(item.Key)] = CraftingData.FormatPointString("0,0");
                }
                File.WriteAllText(settingFile, JsonConvert.SerializeObject(skillPoint, Formatting.Indented));
            }
            return JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(settingFile));
        }

        private void SaveSettings()
        {
            File.WriteAllText(settingFile, JsonConvert.SerializeObject(skillPoint, Formatting.Indented));
        }
    }
    public class SettingsControlSet
    {
        public int id;
        public Label lbl;
        public TextBox tBox;

        public SettingsControlSet() { }
        public SettingsControlSet(int id, Label lbl, TextBox tBox)
        {
            this.id = id;
            this.lbl = lbl;
            this.tBox = tBox;
        } 
    }
}
