using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf
{
    internal class Keymap
    {
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        private const uint MAPVK_VK_TO_CHAR = 2;
        private Dictionary<uint, string> keyMap;

        /// <summary>
        /// 123
        /// </summary>
        public Keymap()
        {
            keyMap = new Dictionary<uint, string>();
        }

        /// <summary>
        /// TODO: 
        /// </summary>
        public static void SaveToJson()
        {
            Dictionary<uint, string> keyMap = new Dictionary<uint, string>();
            for (uint vk = 0; vk <= 255; vk++)
            {
                uint charCode = MapVirtualKey(vk, MAPVK_VK_TO_CHAR);
                if (charCode != 0)
                {
                    keyMap[vk] = ((char)charCode).ToString();
                }
            }

            // 添加其他键的虚拟键码
            keyMap[0x1B] = "VK_ESCAPE"; // ESC
            keyMap[0x0D] = "VK_ENTER"; // Enter
            keyMap[0x20] = "VK_SPACE"; // Space
            keyMap[0x25] = "VK_LEFT"; // Left Arrow
            keyMap[0x26] = "VK_UP"; // Up Arrow
            keyMap[0x27] = "VK_RIGHT"; // Right Arrow
            keyMap[0x28] = "VK_DOWN"; // Down Arrow

            // 将字典内容序列化为 JSON
            string json = JsonConvert.SerializeObject(keyMap, Formatting.Indented);

            // 保存 JSON 到文件
            File.WriteAllText("KeyMap.json", json);

            Console.WriteLine("字典内容已保存为 KeyMap.json");
        }
    }
}
