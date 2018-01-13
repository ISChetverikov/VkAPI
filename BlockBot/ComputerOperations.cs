using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BlockBot
{
    static class ComputerOperations
    {
        [DllImport("user32")]
        private static extern void LockWorkStation();

        public static void Lock()
        {
            LockWorkStation();
        }
    }
}
