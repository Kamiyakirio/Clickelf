using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClickElf.Screen
{
    internal class Screenshot
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern uint GetPixel(IntPtr hdc, int x, int y);
        public static void Shot(Point topLeft, Point bottomRight)
        {
            // 定义左上角和右下角坐标
            //Point topLeft = new Point(100, 100);
            //Point bottomRight = new Point(900, 700);

            // 计算截图区域的宽高

            int width = Math.Abs(bottomRight.X - topLeft.X);
            int height = Math.Abs(bottomRight.Y - topLeft.Y);

            using (var bitmap = new Bitmap(width, height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(topLeft, Point.Empty, new Size(width, height));
                }
                bitmap.Save("custom_screenshot.png");
            }
        }

        public static Color GetColor(Point point)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, point.X, point.Y);
            ReleaseDC(IntPtr.Zero, hdc);

            Color color = Color.FromArgb(
                (int)(pixel & 0x000000FF),
                (int)((pixel & 0x0000FF00) >> 8),
                (int)((pixel & 0x00FF0000) >> 16)
            );

            return color;
        }
    }
}
