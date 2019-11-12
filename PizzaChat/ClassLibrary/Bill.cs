using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Bill : Menu
    {
        public bool _doubleCheese;
        public int _amount;

        public Bill(string name, string structure, int cost, bool cheese, int amount)
             : base(name, structure, cost)
        {
            _doubleCheese = cheese;
            _amount = amount;
        }

        public static string GetOrder(Dictionary<byte, Bill> Order)
        {
            string cheese;
            string order = String.Empty;
            foreach (KeyValuePair<byte, Bill> keyValue in Order)
            {
                if (keyValue.Value._doubleCheese == true){ cheese = "да"; }
                else { cheese = "нет"; }
                if (order != String.Empty) order += "|";
                order += $"№:{keyValue.Key}; название:{keyValue.Value._name}; двойной сыр: {cheese}; цена: {keyValue.Value._cost}; количество: {keyValue.Value._amount}";
            }

            return order;
        }

        public static void AddPizza(Dictionary<byte, Bill> Order, Dictionary<byte, ClassLibrary.Menu> MenuPizza, string pizzaInfo) 
        {
            string[] arrOrder = pizzaInfo.Split(new char[] { ';' });

            string pizzaName = MenuPizza[Convert.ToByte(arrOrder[0])]._name;
            string pizzaStructure = MenuPizza[Convert.ToByte(arrOrder[0])]._structure;
            int pizzaCost = MenuPizza[Convert.ToByte(arrOrder[0])]._cost;
            bool pizzaCheese=false;

            switch (arrOrder[1])
            {
                case "да":
                    pizzaCheese = true;
                    break;
                case "нет":
                    pizzaCheese = false;
                    break;
                default:
                    break;
            }

            byte id;

            if (Order.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = (byte)Order.Keys.Max();
                id++;
            }
            

            Order.Add(id, new Bill(pizzaName, pizzaStructure, pizzaCost, pizzaCheese, Convert.ToInt32(arrOrder[2])));
        }

        public static int BillSum(Dictionary<byte, Bill> Order)
        {
            int sum = 0;
            foreach (KeyValuePair<byte, Bill> keyValue in Order)
            {
                sum += Order[keyValue.Key]._cost * Order[keyValue.Key]._amount;
            }

            return sum;
        }

    }
}
