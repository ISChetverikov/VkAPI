﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBot
{
    interface IPlayer
    {
        void Start();
        void Pause();
        void Quit();
    }
}
