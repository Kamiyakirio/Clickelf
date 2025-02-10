using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Collections.Generic;

public class MouseTracker
{
    // 定义钩子委托
    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    // DLL导入
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    private const int WH_MOUSE_LL = 14;
    private const int WM_MOUSEMOVE = 0x0200;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_RBUTTONDOWN = 0x0207;
    private const int WM_RBUTTONUP = 0x0208;

    private LowLevelMouseProc _proc;
    private IntPtr _hookID = IntPtr.Zero;

    public event Action<int, int> MouseMoved; // 定义鼠标移动事件
    public event Action<Point, bool, int> MousePress;

    public MouseTracker()
    {
        _proc = HookCallback;
        _hookID = SetHook(_proc);
    }

    ~MouseTracker()
    {
        UnhookMouse();
    }

    private IntPtr SetHook(LowLevelMouseProc proc)
    {
        using (var curProcess = Process.GetCurrentProcess())
        using (var curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private void UnhookMouse()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
        {
            var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            MouseMoved?.Invoke(mouseHookStruct.pt.x, mouseHookStruct.pt.y); // 触发事件
        }
        if (nCode >= 0)
        {
            var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            var position = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);

            switch ((int)wParam)
            {
                case WM_LBUTTONDOWN:
                    MousePress?.Invoke(position, true, 0);
                    break;
                case WM_LBUTTONUP:
                    MousePress?.Invoke(position, false, 0);
                    break;
                case WM_RBUTTONDOWN:
                    MousePress?.Invoke(position, true, 1);
                    break;
                case WM_RBUTTONUP:
                    MousePress?.Invoke(position, false, 1);
                    break;
            }
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    public void Dispose()
    {
        UnhookMouse();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}