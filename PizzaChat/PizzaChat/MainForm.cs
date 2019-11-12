﻿using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using ClassLibrary;
using System.Diagnostics;

namespace PizzaChat
{
    public interface IMainForm
    {
        string DialogBox { get; }
        string MsgBox { get; set; }
    }

    public partial class MainForm : Form, IMainForm
    {
        string name;
        byte id;
        string email;
        bool mailing=true;
        string billInfo;
        public string DialogStatus=Constants.DialogStatus01;
        const int pauseTime_ms = 5000;

        readonly Dictionary<byte, Bill> Order = new Dictionary<byte, Bill>();
        readonly Dictionary<byte, Person> People = new Dictionary<byte, Person>();
        readonly Dictionary<byte, ClassLibrary.Menu> MenuPizza = new Dictionary<byte, ClassLibrary.Menu>();

        public MainForm()
        {
            InitializeComponent();
            Constants.CreateDictionary(People, MenuPizza);
            fldDialogBox.AppendText(Constants.DialogMsg01);

            btSendMsg.Click += new EventHandler(BtSendMsg_Click);
        }

        public void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void SendSystemMsg(string msg)
        {
            fldDialogBox.AppendText("\n\n" + msg);
        }

        public void BtSendMsg_Click(object sender, EventArgs e)
        {
            if (fldMsgBox.Text.Length > 0)
            {
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Italic);
                fldDialogBox.AppendText("\n\n" + fldMsgBox.Text);
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Regular);
            }

            switch (DialogStatus)
            {
                case Constants.DialogStatus01:
                    name = fldMsgBox.Text;
                                       
                    SendSystemMsg(Constants.DialogMsg02);
                    ShowMenu();

                    if (name == Constants.AdminName)
                    {
                        DialogStatus = Constants.DialogStatusAdmin01;
                        SendSystemMsg(Constants.DialogMsg04);
                    }
                    else
                    {
                        DialogStatus = Constants.DialogStatus02;
                        SendSystemMsg(Constants.DialogMsg13);
                    }
                    break;
                case Constants.DialogStatusAdmin01:
                    if (Convert.ToInt32(fldMsgBox.Text)>0 && Convert.ToInt32(fldMsgBox.Text) <= Constants.AmountAdminVariants)
                    {
                        switch (fldMsgBox.Text)
                        {
                            case "1":
                                DialogStatus = Constants.DialogStatusAdmin02;
                                SendSystemMsg(Constants.DialogMsg07);
                                break;
                            case "2":
                                DialogStatus = Constants.DialogStatusAdmin03;
                                SendSystemMsg(Constants.DialogMsg11);
                                break;
                            case "3":
                                DialogStatus = Constants.DialogStatusAdmin04;
                                SendSystemMsg(Constants.DialogMsg09);
                                break;
                            case "4":
                                ShowMenu();
                                SendSystemMsg(Constants.DialogMsg04);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        SendSystemMsg(Constants.DialogMsg05);
                        SendSystemMsg(Constants.DialogMsg04);
                        //Ошибка вывода сообщения.
                    }
                    break;
                case Constants.DialogStatusAdmin02:
                    ClassLibrary.Menu.CreatePizza(MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg08);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    break;
                case Constants.DialogStatusAdmin03:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра для цены или id
                    ClassLibrary.Menu.UpdateMenu(MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg12);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    break;
                case Constants.DialogStatusAdmin04:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра
                    ClassLibrary.Menu.DeletePizza(MenuPizza, Convert.ToByte(fldMsgBox.Text));
                    SendSystemMsg(Constants.DialogMsg10);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    break;
                case Constants.DialogStatus02:
                    ClassLibrary.Bill.AddPizza(Order, MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg14);
                    DialogStatus = Constants.DialogStatus03;
                    break;
                case Constants.DialogStatus03:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра
                    if (fldMsgBox.Text == "нет")
                    {
                        id = Person.SearchPerson(People, name);
                        if (id == 0)
                        {
                            DialogStatus = Constants.DialogStatus04;
                            SendSystemMsg(Constants.DialogMsg15);
                            //string name, string email,bool mailing
                        }
                        else
                        {
                            DialogStatus = Constants.DialogStatus06;
                            SendSystemMsg(Constants.DialogMsg19);
                            ShowOrder();
                            email = Person.SearchPersonEmail(People, id);
                            SendMailing(email);
                         }
                    }
                    else
                    {
                        DialogStatus = Constants.DialogStatus02;
                        SendSystemMsg(Constants.DialogMsg13);
                    };
                    
                    break;
                case Constants.DialogStatus04:
                    bool correctEmail = IsValidEmail(fldMsgBox.Text);
                    if (correctEmail == false)
                    {
                        SendSystemMsg(Constants.DialogMsg16);
                    }
                    else
                    {
                        email = fldMsgBox.Text;
                        DialogStatus = Constants.DialogStatus05;
                        SendSystemMsg(Constants.DialogMsg17);
                    }
                    break;
                case Constants.DialogStatus05:
                    switch (fldMsgBox.Text)
                    {
                        case "нет":
                            mailing = false;
                            break;
                        case "да":
                            mailing = true;
                            break;
                        default:
                            break;
                    }

                    Person.CreateNewPerson(People, name, email, mailing);
                    SendSystemMsg(Constants.DialogMsg18);
                    SendSystemMsg(Constants.DialogMsg19);
                    DialogStatus = Constants.DialogStatus06;
                    ShowOrder();
                    SendMailing(email);
                        break;
                default: 
                        break;
            }
            fldMsgBox.Text = String.Empty;
        }

        public void SendMailing(string email)
        {
            SendSystemMsg(Constants.DialogMsg21);
            Email.EmailOrderPayment(email, billInfo);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg22);
            Email.EmailOrderComplited(email, billInfo);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg23);
            Email.EmailOrderDeliveredByCourier(email, billInfo);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void ShowOrder()
        {
            double sumOrder= Bill.BillSum(Order);
            string stringOrder = ClassLibrary.Bill.GetOrder(Order);
            stringOrder= stringOrder.Replace("|", "\n");
            SendSystemMsg(stringOrder);

            billInfo = stringOrder + Constants.DialogMsg20 + sumOrder;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                billInfo += Constants.DialogMsg24;
            }
                
            SendSystemMsg(Constants.DialogMsg20 + sumOrder);
        }

        public void ShowMenu()
        {
            string stringMenu = ClassLibrary.Menu.GetMenu(MenuPizza);
            string[] arrMenu = stringMenu.Split(new char[] { '|' });

            for (int i = 0; i < arrMenu.Length; i++)
            {
                arrMenu[i] = arrMenu[i].Replace(";", "\n");
                SendSystemMsg(arrMenu[i]);
            }
        }

        public string DialogBox
        {
            get { return fldDialogBox.Text; }
        }

        public string MsgBox
        {
            set {fldMsgBox.Text = value; }
            get { return fldMsgBox.Text;}
        }

        //public event EventHandler SendMsg;

        private void PauseMaker(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private void FldMsgBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
