using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person
    {
        public string _name;
        public string _email;
        public bool _mailing;

        public Person(string name, string email, bool mailing)
        {
            _name = name;
            _email = email;
            _mailing = mailing;
        }

        public static string SearchPersonName(Dictionary<byte, ClassLibrary.Person> People, byte id)
        {
            string name = People[id]._name;
            return name;
        }

        public static string SearchPersonEmail(Dictionary<byte, ClassLibrary.Person> People, byte id)
        {
            string email = People[id]._email;
            return email;
        }

        public static bool SearchPersonMailing(Dictionary<byte, ClassLibrary.Person> People, byte id)
        {
            bool mailing = People[id]._mailing;
            return mailing;
        }

        public static byte SearchPerson(Dictionary<byte, ClassLibrary.Person> People, string name)
        {
            byte id = 0;
            for (byte i = 1; i < People.Count; i++)
            {
                if (People[i]._name == name) id=i;
            }
            return id;
        }

        public static byte CreateNewPerson(Dictionary<byte, ClassLibrary.Person> People, string name, string email,bool mailing)
        {
            byte id = (byte)People.Keys.Max();
            id++;

            People.Add(id, new ClassLibrary.Person(name, email, mailing)) ;

            return id;
        }
    }
}
