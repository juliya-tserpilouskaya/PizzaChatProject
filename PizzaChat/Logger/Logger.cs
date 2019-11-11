using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void INFOLog(string message);
        void ERRORLog(string message);
        void WARNLog(string message);        
    }
    public class Logger : ILogger
    {
        static void Main()
        {
            
        }
        
        public Logger()
        {

        }
        public void INFOLog(string message)
        {
            //TODO: запись в файл сделать
            Console.WriteLine(message);
        }
        public void ERRORLog(string message)
        {
            //TODO: запись в файл сделать
            Console.WriteLine(message);
        }
        public void WARNLog(string message)
        {
            //TODO: запись в файл сделать
            Console.WriteLine(message);
        }
    }
}
