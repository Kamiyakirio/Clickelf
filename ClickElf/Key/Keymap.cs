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
        private static string KMB64 = "ew0KICAgICIwIjogIjQ4IiwNCiAgICAiMSI6ICI0OSIsDQogICAgIjIiOiAiNTAiLA0KICAgICIzIjogIjUxIiwNCiAgICAiNCI6ICI1MiIsDQogICAgIjUiOiAiNTMiLA0KICAgICI2IjogIjU0IiwNCiAgICAiNyI6ICI1NSIsDQogICAgIjgiOiAiNTYiLA0KICAgICI5IjogIjU3IiwNCiAgICAiQSI6ICI2NSIsDQogICAgIkIiOiAiNjYiLA0KICAgICJDIjogIjY3IiwNCiAgICAiRCI6ICI2OCIsDQogICAgIkUiOiAiNjkiLA0KICAgICJGIjogIjcwIiwNCiAgICAiRyI6ICI3MSIsDQogICAgIkgiOiAiNzIiLA0KICAgICJJIjogIjczIiwNCiAgICAiSiI6ICI3NCIsDQogICAgIksiOiAiNzUiLA0KICAgICJMIjogIjc2IiwNCiAgICAiTSI6ICI3NyIsDQogICAgIk4iOiAiNzgiLA0KICAgICJPIjogIjc5IiwNCiAgICAiUCI6ICI4MCIsDQogICAgIlEiOiAiODEiLA0KICAgICJSIjogIjgyIiwNCiAgICAiUyI6ICI4MyIsDQogICAgIlQiOiAiODQiLA0KICAgICJVIjogIjg1IiwNCiAgICAiViI6ICI4NiIsDQogICAgIlciOiAiODciLA0KICAgICJYIjogIjg4IiwNCiAgICAiWSI6ICI4OSIsDQogICAgIloiOiAiOTAiLA0KICAgICIqIjogIjEwNiIsDQogICAgIisiOiAiMTA3IiwNCiAgICAiLSI6ICIxODkiLA0KICAgICIuIjogIjE5MCIsDQogICAgIi8iOiAiMTkxIiwNCiAgICAiOyI6ICIxODYiLA0KICAgICI9IjogIjE4NyIsDQogICAgIiwiOiAiMTg4IiwNCiAgICAiYCI6ICIxOTIiLA0KICAgICJbIjogIjIxOSIsDQogICAgIlxcIjogIjIyMCIsDQogICAgIl0iOiAiMjIxIiwNCiAgICAiJyI6ICIyMjIiLA0KICAgICJMU2hpZnRLZXkiOiAiMTYwIiwNCiAgICAiTEN0cmxLZXkiOiAiMTYyIiwNCiAgICAiTE1lbnUiOiAiMTY0Ig0KfQ==";

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
            var keyMapFromBase64 = System.Convert.FromBase64String(KMB64);
            File.WriteAllBytes("KeyMap.json",keyMapFromBase64);
        }
    }
}
