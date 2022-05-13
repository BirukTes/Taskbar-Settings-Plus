using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TS__Configurator
{

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                left = Left;
                top = Top;
                right = Right;
                bottom = Bottom;
            }
        }

        public const int WS_EX_LAYERED = 0x00080000;
        public const int WM_ERASEBKGND = 0x0014;
        public const uint LWA_COLORKEY = 0x00000001;
        public const uint LWA_ALPHA = 0x00000002;
        const int GWL_EXSTYLE = (-20);

        IntPtr hWnd = IntPtr.Zero;

        public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);
        private SUBCLASSPROC SubClassDelegate;

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("User32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern long GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern long GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        public static long GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool FillRect(IntPtr hdc, [In] ref RECT rect, IntPtr hbrush);

        [DllImport("Gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        public MainWindow()
        {
            this.InitializeComponent();
            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
            bool bRet = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            long nExStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            if ((nExStyle & WS_EX_LAYERED) == 0)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, (IntPtr)(nExStyle | WS_EX_LAYERED));
                bool bReturn = SetLayeredWindowAttributes(hWnd, (uint)System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Magenta), 255, LWA_COLORKEY);
            }
        }
        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("Gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeleteObject([In] IntPtr hObject);
        private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
        {
            switch (uMsg)
            {
                case WM_ERASEBKGND:
                    {
                        RECT rect;
                        GetClientRect(hWnd, out rect);
                        IntPtr hBrush = CreateSolidBrush(System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Magenta));
                        //IntPtr hBrush = CreateSolidBrush((int)MakeArgb(255, 255, 0, 0));                       
                        //IntPtr hBrush = CreateSolidBrush(System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.FromArgb(255, 32, 32, 32)));
                        FillRect(wParam, ref rect, hBrush);
                        DeleteObject(hBrush);
                        return 1;
                    }
            }
            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }
    }
}
