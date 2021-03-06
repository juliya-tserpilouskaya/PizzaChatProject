﻿using System;
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

        public static string GetMenu(Dictionary<byte, ClassLibrary.Menu> MenuPizza)
        {
            string menu = String.Empty;
            foreach (KeyValuePair<byte, Menu> keyValue in MenuPizza)
            {
                if (menu != String.Empty) menu += "|";
                menu += $"№:{keyValue.Key};название:{keyValue.Value._name};состав:{keyValue.Value._structure};цена: {keyValue.Value._cost}";
            }
            return menu;
        }
        public static bool GetPizza (Dictionary<byte, ClassLibrary.Menu> MenuPizza, byte id)
        {
            bool total = false;

            foreach (KeyValuePair<byte, Menu> keyValue in MenuPizza)
            {
                if (keyValue.Key == id) { total = true; }
            }
             return total;
        }

        public static void CreatePizza(Dictionary<byte, ClassLibrary.Menu> MenuPizza, string pizzaInfo)
        {
            string[] arrMenu = pizzaInfo.Split(new char[] { ';' });
            byte id = (byte)MenuPizza.Keys.Max();
            id++;
            MenuPizza.Add(id, new Menu(arrMenu[0], arrMenu[1], Convert.ToInt32(arrMenu[2])));
        }

        public static void UpdateMenu(Dictionary<byte, ClassLibrary.Menu> MenuPizza, string pizzaInfo)
        {
            string[] arrMenu = pizzaInfo.Split(new char[] { ';' });
            MenuPizza[Convert.ToByte(arrMenu[0])] = new Menu(arrMenu[1], arrMenu[2], Convert.ToInt32(arrMenu[3]));
        }

        public static void DeletePizza(Dictionary<byte, ClassLibrary.Menu> MenuPizza, byte id)
        {
            MenuPizza.Remove(id);
        }
    }
}
