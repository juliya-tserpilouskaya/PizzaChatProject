using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Menu
    {
        public string _name;
        public string _structure;
        public int _cost;

        public Menu(string name, string structure, int cost)
        {
            _name = name;
            _structure = structure;
            _cost = cost;
        }

        public static string GetMotorcycles(Dictionary<byte, ClassLibrary.Menu> MenuPizza)
        {
            string menu = String.Empty;
            foreach (KeyValuePair<byte, Menu> keyValue in MenuPizza)
            {
                menu +="|" + $"id: {keyValue.Key}, название:{keyValue.Value._name}, состав:{keyValue.Value._structure}, цена: {keyValue.Value._cost}.";
            }

            return menu;
        }

        public static void CreatePizza(Dictionary<byte, ClassLibrary.Menu> MenuPizza, string name, string structure, int cost)
        {
            byte id = (byte)MenuPizza.Keys.Max();
            id++;

            MenuPizza.Add(id, new Menu(name, structure, cost));
        }

        public static void UpdateMenu(Dictionary<byte, ClassLibrary.Menu> MenuPizza, byte id, string name, string structure, int cost )
        {
            MenuPizza[id] = new Menu(name, structure, cost);
        }

        public static void DeletePizza(Dictionary<byte, ClassLibrary.Menu> MenuPizza, byte id)
        {
            MenuPizza.Remove(id);
        }
    }
}
