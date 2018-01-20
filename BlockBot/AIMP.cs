using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace BlockBot
{
    class AIMP : IPlayer
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string sClass, string sWindow);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        const string AIMPRemoteAccessClass = "AIMP2_RemoteInfo";

        public void Pause()
        {
            var Window = FindWindow(AIMPRemoteAccessClass, null);
            var Result = SendMessage(
                Window, 
                (int)AimpWindowsMessages.WM_AIMP_COMMAND, 
                new IntPtr((int)AimpCommands.AIMP_RA_CMD_PAUSE),
                IntPtr.Zero
            );
        }

        public void Quit()
        {
            var Window = FindWindow(AIMPRemoteAccessClass, null);
            var Result = SendMessage(
                Window,
                (int)AimpWindowsMessages.WM_AIMP_COMMAND,
                new IntPtr((int)AimpCommands.AIMP_RA_CMD_QUIT),
                IntPtr.Zero
            );
        }

        public void Start()
        {
            var path = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\Media\AIMP\shell\open\command")?.GetValue(null)?.ToString();
            if (path == null)
                throw new ApplicationException("AIMP has not been installed.");

            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("Executable file of AIMP has not been found.");
           
            Process.Start(path);
        }

    }
}
