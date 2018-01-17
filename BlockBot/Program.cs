using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlockBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot((text) => Console.WriteLine(text));

            bot.Initialize();
            bot.Run();
        }
    }
}
