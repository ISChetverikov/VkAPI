using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BlockBot
{
    static class Computer
    {
        [DllImport("user32")]
        private static extern void LockWorkStation();

        private static IPlayer _player = new AIMP();
        public static IPlayer Player
        {
            set
            {
                _player = value;
            }
            get
            {
                return _player;
            }
        }
        
        public static void Lock()
        {
            LockWorkStation();
        }
        
    }
}
