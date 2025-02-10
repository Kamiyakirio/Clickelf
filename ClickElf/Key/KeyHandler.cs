using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf.Key
{
    internal class KeyHandler : IDisposable
    {

        private Dictionary<string, uint> keyMap;
        public KeyHook keyHook;
        private const uint KEYEVENTF_KEYUP = 0x0002; // 键盘按键抬起的标志

        public KeyHandler()
        {
            LoadVKeysFromJson();
            keyHook = new KeyHook();
        }

        public void Dispose()
        {
            keyHook.Dispose();
        }

        private void LoadVKeysFromJson()
        {
            string filePath = "KeyMap.json"; // JSON 文件路径

            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                Console.WriteLine("文件未找到.");
                Keymap.SaveToJson();
                //return;
            }
            // 从 JSON 文件读取并反序列化为字典
            string json = File.ReadAllText(filePath);
            keyMap = JsonConvert.DeserializeObject<Dictionary<string, uint>>(json);
        }

        public Dictionary<string, uint> Getmap() { return keyMap; }

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        private const int INPUT_KEYBOARD = 1;

        #region DataStructures
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public MOUSEINPUT mi;
        }
        [StructLayout(LayoutKind.Sequential)]
        #endregion
        struct INPUT
        {
            public uint type;
            public MOUSEKEYBDHARDWAREINPUT mkhi;
        }


        /// <summary>
        /// 产生键盘事件
        /// </summary>
        /// <param name="VK">虚拟键码</param>
        /// <param name="type">事件类型: 0 - 按下, 1 - 抬起</param>
        private uint LetKeyboardEvent(ushort VK, int type)
        {
            if (type != 0 && type != 1) { unchecked { MessageBox.Show("Runtime Error!"); return (uint)-1; } }
            INPUT[] input = new INPUT[1];
            input[0] = new INPUT
            {
                type = 1,
                mkhi = new MOUSEKEYBDHARDWAREINPUT
                {
                    ki = new KEYBDINPUT
                    {
                        dwFlags = (uint)(type*2),
                        wVk = VK
                    }
                }
            };
            uint result = SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
            return result;
        }
        public uint SendKey(ushort VK)
        {
            INPUT[] input = new INPUT[1];
            input[0] = new INPUT
            {
                type = 1,
                mkhi = new MOUSEKEYBDHARDWAREINPUT
                {
                    ki = new KEYBDINPUT
                    {
                        dwFlags = 0,
                        wVk = VK
                    }
                }
            };
            uint result = SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            // await Task.Delay(50);

            // 模拟释放 'A' 键
            input[0].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // 抬起按键
            SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            //Console.WriteLine(result);

            return result;
        }
        public uint SendKey(uint VK)
        {
            INPUT[] input = new INPUT[1];
            input[0] = new INPUT
            {
                type = 1,
                mkhi = new MOUSEKEYBDHARDWAREINPUT
                {
                    ki = new KEYBDINPUT
                    {
                        dwFlags = 0,
                        wVk = (ushort)VK
                    }
                }
            };
            uint result = SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            // await Task.Delay(50);

            // 模拟释放 'A' 键
            input[0].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // 抬起按键
            SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            //Console.WriteLine(result);

            return result;
        }

        public async Task<uint> SendKeyAwait(ushort VK, int delay)
        {
            await Task.Delay(delay);

            INPUT[] input = new INPUT[1];
            input[0] = new INPUT
            {
                type = 1,
                mkhi = new MOUSEKEYBDHARDWAREINPUT
                {
                    ki = new KEYBDINPUT
                    {
                        dwFlags = 0,
                        wVk = VK
                    }
                }
            };
            uint result = SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            // await Task.Delay(50);

            // 模拟释放 'A' 键
            input[0].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // 抬起按键
            SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
            return result;
        }

        private void ReleaseKey(ushort VK)
        {
            //INPUT[] input = new INPUT[1];
            //input[0] = new INPUT
            //{
            //    type = 1,
            //    mkhi = new MOUSEKEYBDHARDWAREINPUT
            //    {
            //        ki = new KEYBDINPUT
            //        {
            //            dwFlags = 2,
            //            wVk = VK
            //        }
            //    }
            //};
            //SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
            LetKeyboardEvent(VK, 1);
        }

        private void PressKey(ushort VK)
        {
            //INPUT[] input = new INPUT[1];
            //input[0] = new INPUT
            //{
            //    type = 1,
            //    mkhi = new MOUSEKEYBDHARDWAREINPUT
            //    {
            //        ki = new KEYBDINPUT
            //        {
            //            dwFlags = 2,
            //            wVk = VK
            //        }
            //    }
            //};
            //SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
            LetKeyboardEvent(VK, 0);
        }

        public void SendDoubleInput(Keys First, Keys Second)
        {
            PressKey((ushort)First);
            PressKey((ushort)Second);
            ReleaseKey((ushort)Second);
            ReleaseKey((ushort)First);
        }
        public async Task SendStringWithEscapesAsync(string input)
        {
            await Task.Delay(500);
            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                // 处理转义字符
                if (currentChar == '\\' && i + 1 < input.Length)
                {
                    char nextChar = char.ToUpper(input[i + 1]);
                    switch (nextChar)
                    {
                        case 's':
                            // 发送 Shift
                            SendKey((ushort)Keys.ShiftKey);
                            i++; // 跳过下一个字符
                            break;
                        case 'c':
                            // 发送 Ctrl
                            SendKey((ushort)Keys.ControlKey);
                            i++;
                            break;
                        case 'a':
                            // 发送 Alt
                            SendKey((ushort)Keys.Menu);
                            i++;
                            break;
                        case '\\':
                            // 发送反斜杠
                            SendKey((ushort)Keys.OemBackslash);
                            i++;
                            break;
                        default:
                            // 发送普通字符
                            if (Enum.TryParse<Keys>(nextChar.ToString(), true, out Keys key))
                            {
                                SendKey((ushort)key);
                                i++;
                            }
                            break;
                    }
                }
                else if (char.IsDigit(currentChar))
                {
                    SendKey((uint)currentChar);
                }
                else if (currentChar == ' ')
                {
                    SendKey((ushort)Keys.Space);
                }
                else
                {
                    // 发送普通字符
                    if (Enum.TryParse<Keys>(currentChar.ToString(), true, out Keys key))
                    {
                        SendKey((ushort)key);
                    }
                }

                await Task.Delay(25); // 设置延迟
            }

            // 释放组合键
            ReleaseKey((ushort)Keys.ShiftKey);
            ReleaseKey((ushort)Keys.ControlKey);
            ReleaseKey((ushort)Keys.Menu);
        }
    }
}
