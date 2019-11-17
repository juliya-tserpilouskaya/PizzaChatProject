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
        public const double TuesdaySale = 0.2;
        public const string DialogMsg01 = "Здравствуйте, представьтесь, пожалуйста.";
        public const string DialogMsg02 = "Oзнакомьтесь с нашим меню:";
        public const string DialogMsg03 = "Г-н администратор, введите номер неободимой команды :";
        public const string DialogMsg04 = DialogMsg03 + "1 - внести в меню новую пиццу; 2 - изменить пиццу; 3 - удалить пиццу из меню; 4 - вывести все меню.";
        public const string DialogMsg05 = "Ошибка выбора варианта ответа.";
        public const string DialogMsg06 = "Администратор ввел неверную команду.";
        public const string DialogMsg07 = "Введине данные о новой пицце согласно шаблону: *название*;*соста*;*цена*";
        public const string DialogMsg08 = "Новая пицца занесена в меню.";
        public const string DialogMsg09 = "Для удаления пиццы введите ее номер";
        public const string DialogMsg10 = "Пицца удалена.";
        public const string DialogMsg11 = "Введине информацию для изменеия согласно шаблону: *номер*;*название*;*соста*;*цена*";
        public const string DialogMsg12 = "Пицца обновлена.";
        public const string DialogMsg13 = "Введите данные о заказе согласно шаблону: *номер пиццы*;*нужен ли двойной сыр (да/нет)*;*количество пицц*.";
        public const string DialogMsg14 = "Пицца добавлена к заказу, желаете ли еще? (да/нет)";
        public const string DialogMsg15 = "Введите почту.";
        public const string DialogMsg16 = "Почта введена не верно, введите ее повторно.";
        public const string DialogMsg17 = "Необходима ли Вам подписка на рассылки? (да/нет)";
        public const string DialogMsg18 = "Пользователь успешно создан.";
        public const string DialogMsg19 = "Информация о заказе.";
        public const string DialogMsg20 = "Сумма счета:";
        public const string DialogMsg21 = "Заказ оплачен.";
        public const string DialogMsg22 = "Заказ скомплектован.";
        public const string DialogMsg23 = "Заказ доставлен курьером.";
        public const string DialogMsg24 = " (C учетом скидки.)";
        public const string DialogMsg25 = "Ответ введен не верно. Попробуйте еще раз.";
        public const int AmountAdminVariants = 4;
        public const string DialogStatus01 = "start";
        public const string DialogStatus02 = "menu";
        public const string DialogStatus03 = "one more?";
        public const string DialogStatus04 = "create email";
        public const string DialogStatus05 = "create mailing";
        public const string DialogStatus06 = "mailing";
        public const string DialogStatusAdmin01 = "admin start";
        public const string DialogStatusAdmin02 = "create new pizza";
        public const string DialogStatusAdmin03 = "change pizza";
        public const string DialogStatusAdmin04 = "delete pizza";

        public static void CreateDictionary(Dictionary<byte, Person> People, Dictionary<byte, Menu> MenuPizza)
        {
            People.Add(1, new Person("Ludmila", "ludmila.sav.96@gmail.com", true));
            People.Add(2, new Person("Julia", "juliet_ai@mail.ru", true));

            MenuPizza.Add(1, new Menu("Пепперони и болгарский перец", "пицца-соус, пепперони, свежий болгарский перец, сыр моцарелла, базилик", 17));
            MenuPizza.Add(2, new Menu("Кантри четыре сезона", "грибной соус, буженина (свинина), грудинка (свинина), маринованные опята, сыр фета, сыр дорблю, сыр моцарелла, базилик", 22));
            MenuPizza.Add(3, new Menu("Баварский цыпленок", "сладкий горчичный соус, грудинка (свинина), филе цыпленка, свежий болгарский перец, маслины, сыр моцарелла, базилик", 20));
            MenuPizza.Add(4, new Menu("Ранч пицца", "американский соус ранч, филе цыпленка, ветчина, свежие томаты, сыр моцарелла, базилик", 17));
            MenuPizza.Add(5, new Menu("Гавайская", "сырный соус, ветчина, филе цыпленка, ананасы, сыр моцарелла, базилик", 15));
            MenuPizza.Add(6, new Menu("Грибная", "чесночный соус, ветчина, свежие шампиньоны, сыр моцарелла, базилик", 17));
        }
    }
}
