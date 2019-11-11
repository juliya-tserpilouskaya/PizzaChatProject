using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logger;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            for (int i = 0; i < 1; i++)
            {
                Program proga = new Program();
                var log = new CustomLogger();
                log.UseLogger("DEBUG", "It's a live!", Thread.GetDomainID().ToString(), log.GetType().ToString());
                //log.UseLogger("INFO", "It's a live!", Thread.GetDomainID().ToString(), proga.GetType().ToString());
                //log.UseLogger("ERROR", "He is dead. Jim!", Thread.GetDomainID().ToString(), proga.GetType().ToString());
                Console.ReadKey();
            }

        }
    }
}
