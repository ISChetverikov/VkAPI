using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBot
{
    enum AimpCommands : int
    {
        AIMP_RA_CMD_BASE = 10,

        AIMP_RA_CMD_PAUSE = AIMP_RA_CMD_BASE + 5,
        AIMP_RA_CMD_QUIT = AIMP_RA_CMD_BASE + 11
    }
}
