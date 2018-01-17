using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace BlockBot
{
    class AIMP : IPlayer
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string sClass, string sWindow);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        const string AIMPRemoteAccessClass = "AIMP2_RemoteInfo";
        const int WM_AIMP_COMMAND = 0x0400 + 0x75;
        const int AIMP_RA_CMD_BASE = 10;
        const int AIMP_RA_CMD_PAUSE = AIMP_RA_CMD_BASE + 5;

        public void Pause()
        {
            var Window = FindWindow(AIMPRemoteAccessClass, null);
            var Result = SendMessage(Window, WM_AIMP_COMMAND, new IntPtr(AIMP_RA_CMD_PAUSE), IntPtr.Zero);
        }

    }
}
