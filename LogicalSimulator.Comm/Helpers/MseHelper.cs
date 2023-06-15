using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cViP.AA
{
    public class MseHelper
    { 
        internal const int MOUSEEVENTF_MOVE = 0x0001;     // mse movement 
        internal const int MOUSEEVENTF_LEFTDOWN = 0x0002; // mse left down 
        internal const int MOUSEEVENTF_LEFTUP = 0x0004; //mse left up
        internal const int MOUSEEVENTF_RIGHTDOWN = 0x0008; // mse right down
        internal const int MOUSEEVENTF_RIGHTUP = 0x0010; // mse left up
        internal const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// mse middle down 
        internal const int MOUSEEVENTF_MIDDLEUP = 0x0040; // mse middle up  
        internal const int MOUSEEVENTF_WHEEL = 0x800;
        internal const int MOUSEEVENTF_ABSOLUTE = 0x8000; // abs or relative? 
        // mse move to x y position
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public extern static bool SetCursorPos(int x, int y);
        // get mse current position
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        [DllImport("user32.dll")]
        public extern static bool GetCursorPos(out POINT p);
        // display arrow?
        [DllImport("user32.dll")]
        public extern static int ShowCursor(bool bShow);
        // mse event
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        internal static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
