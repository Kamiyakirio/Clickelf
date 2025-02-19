using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClickElf.Mouse
{
    internal class MouseHandler
    {
        public MouseTracker mouseTracker;

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x00000002;
        private const uint MOUSEEVENTF_LEFTUP = 0x00000004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x00000008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x00000010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        public MouseHandler()
        {
            mouseTracker = new MouseTracker();
        }

        public void MouseClick(MouseType type)
        {
            switch (type)
            {
                case MouseType.Left:
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case MouseType.Right:
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case MouseType.Middle:
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    break;
                default:
                    throw new ArgumentException();
            }

        }

        public enum MouseType
        {
            Left = 1, Right = 2, Middle = 4
        }

        public bool MouseMove(int X,int Y)
        {
            return SetCursorPos(X,Y);
        }
    }
}
