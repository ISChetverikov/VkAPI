using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AimpControl
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string sClass, string sWindow);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        const string AIMPRemoteAccessClass = "AIMP2_RemoteInfo";
        const int WM_AIMP_COMMAND = 0x0400 + 0x75;
        const int AIMP_RA_CMD_BASE = 10;
        const int AIMP_RA_CMD_PAUSE = AIMP_RA_CMD_BASE + 5;

        static void Main(string[] args)
        {
            var Window = FindWindow(AIMPRemoteAccessClass, null);
            var Result = SendMessage(Window, WM_AIMP_COMMAND, new IntPtr(AIMP_RA_CMD_PAUSE), IntPtr.Zero);
        }
    }
}
