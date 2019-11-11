using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Constants
    {
        public const string FromEmailAddress = "mylt015@mail.ru";
        public const string FromEmailPassword = "perisher1996";
        public const string EmailOrderComplitedTitle = "Заказ скомплетован.";
        public const string EmailOrderComplitedMsgBody = "Здравствуйте, этим письмом мы уведомляем о том, что Ваш заказ скомплектован.";
        public const string EmailOrderDeliveredByCourierTitle = "Заказ доставлен курьером.";
        public const string EmailOrderDeliveredByCourierMsgBody = "Здравствуйте, этим письмом мы уведомляем о том, что Ваш заказ доставлен курьером.";
        public const string EmailOrderPaymentTitle = "Заказ оплачен."; 
        public const string EmailOrderPaymentMsgBody = "Здравствуйте, этим письмом мы уведомляем Вас о получении нами оплаты.";
        public const string AdminName = "admin";
        public const string DilogMsg01 = "Здравствуйте, представьтесь, пожалуйста.";
        public const string DilogMsg02 = "Oзнакомьтесь с нашим меню и чет ответьте";
        public const string DialogStatus01 = "start";
        public const string DialogStatus02 = "menu";
        public const string DialogStatusAdmin01 = "admin start";

        public static void CreateDictionary()
        {
            Dictionary<byte, ClassLibrary.Person> People = new Dictionary<byte, ClassLibrary.Person>();

            People.Add(1, new ClassLibrary.Person("Ludmila", "ludmila.sav.96@gmail.com", true));
            People.Add(2, new ClassLibrary.Person("Ludmila", "ludmila.sav.96@gmail.com", true));

            Dictionary<byte, ClassLibrary.Menu> MenuPizza = new Dictionary<byte, ClassLibrary.Menu>();

            
        }

    }
}
