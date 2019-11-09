using System;
using System.Collections.Generic;
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
        void PersonsLog(OurPerson[] persons);
        
    }
    public class Logger : ILogger
    {
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
        public void PersonsLog(OurPerson[] persons)
        {
            throw new NotImplementedException();
            //Concept in development
        }
    }
    #region Persons class example
    public class OurPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    #endregion
}
