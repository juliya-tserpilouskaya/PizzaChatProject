using ClassLibrary;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {

            Dictionary<byte, ClassLibrary.Person> People = new Dictionary<byte, ClassLibrary.Person>();

            People.Add(1, new ClassLibrary.Person("admin", "email1",false));
            People.Add(2, new ClassLibrary.Person("name2", "email2",true));

            Dictionary<byte, ClassLibrary.Menu> MenuPizza = new Dictionary<byte, ClassLibrary.Menu>();


            Console.WriteLine("Hello World!");
        }       
    }
}
    