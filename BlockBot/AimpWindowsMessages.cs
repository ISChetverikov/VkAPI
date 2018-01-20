using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBot
{
    enum AimpWindowsMessages : int
    {
        WM_AIMP_COMMAND = WindowsMessages.WM_USER + 0x75,
        WM_AIMP_NOTIFY = WindowsMessages.WM_USER + 0x76,
        WM_AIMP_PROPERTY = WindowsMessages.WM_USER + 0x77,
    }
}
