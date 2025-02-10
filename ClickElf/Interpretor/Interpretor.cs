using ClickElf.Key;
using ClickElf.Mouse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf.Interpretor
{

    internal struct ExecuteArgs
    {
        KeyHandler keyHandler;
        MouseHandler mouseHandler;
    }
    internal partial class Interpretor
    {
        private List<string> lines;
        private KeyHandler keyHandler;
        private MouseHandler mouseHandler;

        public Interpretor(string filePath, KeyHandler keyHandler, MouseHandler mouseHandler)
        {
            lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    line = line.Trim();
                    string[] foo = line.Split(';');
                    foreach (var item in foo)
                    {
                        if (item != "") lines.Add(item);
                    }
                }
            }
            this.keyHandler = keyHandler;
            this.mouseHandler = mouseHandler;
            variables = new Dictionary<string, string>();
        }
        public void ExecuteLine(string line, int column)
        {
            if (line == "" || string.IsNullOrWhiteSpace(line) || line.StartsWith("//")) return;
            string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string command = tokens[0].ToLower();
            for (int i = 1; i < tokens.Length; i++)
            {
                tokens[i] = ReplaceVariables(tokens[i]);  // 替换变量值
            }
            switch (command)
            {
                case "mv": // Mouse Move
                    if (tokens.Length != 3) throw new ArgumentException("Mv的参数只能为2个");
                    int x, y;
                    if (!int.TryParse(tokens[1], out x) || !int.TryParse(tokens[2], out y))
                    {
                        throw new ArgumentException("无法被解析为合理的数值");
                    }
                    mouseHandler.MouseMove(x, y);
                    break;
                case "w": // Wait
                    if (tokens.Length != 2) throw new ArgumentException("w的参数只能为1个");
                    int sleep;
                    if (!int.TryParse(tokens[1], out sleep)) throw new ArgumentException("无法被解析为合理的数值");
                    Thread.Sleep(sleep);
                    break;
                case "pk": // Press Keyboard
                    if (tokens.Length != 3) throw new ArgumentException("PK的参数只能为2个");
                    int vkCode = -1;
                    string mode = tokens[2].ToLower();
                    // l = literal, c = code
                    if (mode != "l" && mode != "c")
                    {
                        throw new ArgumentException("模式只能为 l (for Literal) 或 c (for virtual key Code)");
                    }
                    if (mode == "c")
                    {
                        if (!int.TryParse(tokens[1], out x))
                        {
                            throw new ArgumentException("无法被解析为合理的数值");
                        }
                        keyHandler.SendKey((uint)vkCode);
                    }
                    else if (mode == "l")
                    {
                        string litkey = tokens[1];
                        string regex = "^(?:[a-z0-9]|(?:[SCA][a-z0-9]))+$";
                        Dictionary<string, uint> keymap = keyHandler.Getmap();
                        if (!Regex.IsMatch(litkey, regex))
                        {
                            throw new ArgumentException("不合法的输入");
                        }
                        litkey = litkey.ToUpper();
                        if (litkey.Length == 1)
                        {
                            keyHandler.SendKey(keymap[litkey]);
                        }
                        else if (litkey.Length == 2)
                        {
                            if (litkey[0] == 'S') keyHandler.SendDoubleInput(Keys.LShiftKey, (Keys)keymap[litkey[1].ToString()]);
                            else if (litkey[0] == 'C') keyHandler.SendDoubleInput(Keys.LControlKey, (Keys)keymap[litkey[1].ToString()]);
                            else if (litkey[0] == 'A') keyHandler.SendDoubleInput(Keys.LMenu, (Keys)keymap[litkey[1].ToString()]);
                        }
                    }
                    break;
                case "mc": // Mouse Click
                    if (tokens.Length != 2)
                    {
                        throw new ArgumentException("mc的参数只能为2个");
                    }
                    string btn = tokens[1];
                    if (!new List<string> { "0", "1", "2", "l", "m", "r" }.Contains(btn))
                    {
                        throw new ArgumentException("使用合理的按键代码 (0, 1, 2, l, m, r)");

                    }
                    if (btn == "0" || btn == "l")
                    {
                        mouseHandler.MouseClick(MouseHandler.MouseType.Left);
                    }
                    else if (btn == "1" || btn == "m")
                    {
                        mouseHandler.MouseClick(MouseHandler.MouseType.Middle);
                    }
                    else if (btn == "2" || btn == "r")
                    {
                        mouseHandler.MouseClick(MouseHandler.MouseType.Right);
                    }
                    break;
                case "set":
                    if (tokens.Length < 3)
                        throw new ArgumentException($"set 需要至少 2 个参数！");
                    else if (tokens.Length > 3)
                    {
                        string expression = "";
                        for (int i = 2; i < tokens.Length; ++i) { expression += tokens[i]; }
                        if (!expression.Any(item => new char[] { '+', '-', '*', '/' }.Contains(item)))
                        {
                            throw new ArgumentException($"解析错误 in column {column}");
                        }
                        string varName = tokens[1];
                        string varValue=EvaluateMathExpression(expression).ToString();
                        variables[varName] = varValue;
                    }
                    else
                    {
                        string firstParam = tokens[1], secondParam = tokens[2];
                        variables[firstParam] = secondParam;
                    }
                    break;
                default:
                    if (!tokens[0].StartsWith("//"))
                        throw new ArgumentException($"无法解析的参数 in column {column}");
                    break;
            }
        }

        public void Execute()
        {
            for (int i = 0; i < lines.Count; i++) { ExecuteLine(lines[i], i + 1); }
        }

        public List<string> GetLines() { return lines; }
    }
}
