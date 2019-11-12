using System.Net;
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
using Logger;
using System.Threading;
using System.Runtime.CompilerServices;

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

        readonly CustomLogger logger = new CustomLogger();

        public MainForm()
        {
            InitializeComponent();
            Constants.CreateDictionary(People, MenuPizza);
            fldDialogBox.AppendText(Constants.DialogMsg01);
            btSendMsg.Click += new EventHandler(BtSendMsg_Click); //event
        }

        public void MainForm_Load(object sender, EventArgs e) { }

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
                        logger.UseLogger("INFO", "Admin administrates...", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }
                    else
                    {
                        DialogStatus = Constants.DialogStatus02;
                        SendSystemMsg(Constants.DialogMsg13);
                        logger.UseLogger("INFO", "Be our guest! Put our service to the test!", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
                                logger.UseLogger("INFO", "Start" + Constants.DialogStatusAdmin02, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                break;
                            case "2":
                                DialogStatus = Constants.DialogStatusAdmin03;
                                SendSystemMsg(Constants.DialogMsg11);
                                logger.UseLogger("INFO", "Start" + Constants.DialogStatusAdmin03, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                break;
                            case "3":
                                DialogStatus = Constants.DialogStatusAdmin04;
                                SendSystemMsg(Constants.DialogMsg09);
                                logger.UseLogger("INFO", "Start" + Constants.DialogStatusAdmin04, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                break;
                            case "4":
                                ShowMenu();
                                SendSystemMsg(Constants.DialogMsg04);
                                logger.UseLogger("INFO", "Update menu list.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                break;
                        }
                    }
                    else
                    {
                        SendSystemMsg(Constants.DialogMsg05);
                        SendSystemMsg(Constants.DialogMsg04);
                        logger.UseLogger("DEBUG", Constants.DialogMsg06, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }
                    break;
                case Constants.DialogStatusAdmin02:
                    ClassLibrary.Menu.CreatePizza(MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg08);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    logger.UseLogger("INFO", Constants.DialogStatusAdmin02 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    break;
                case Constants.DialogStatusAdmin03:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра для цены или id
                    ClassLibrary.Menu.UpdateMenu(MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg12);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    logger.UseLogger("INFO", Constants.DialogStatusAdmin03 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    break;
                case Constants.DialogStatusAdmin04:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра
                    ClassLibrary.Menu.DeletePizza(MenuPizza, Convert.ToByte(fldMsgBox.Text));
                    SendSystemMsg(Constants.DialogMsg10);
                    SendSystemMsg(Constants.DialogMsg04);
                    DialogStatus = Constants.DialogStatusAdmin01;
                    logger.UseLogger("INFO", Constants.DialogStatusAdmin04 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    break;
                case Constants.DialogStatus02:
                    ClassLibrary.Bill.AddPizza(Order, MenuPizza, fldMsgBox.Text);
                    SendSystemMsg(Constants.DialogMsg14);
                    DialogStatus = Constants.DialogStatus03;
                    logger.UseLogger("INFO", "Menu list updated." , Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    break;
                case Constants.DialogStatus03:
                    //Ошибка: проверить есть ли вообще такой номер в id библиотеки
                    //Ошибка: введена не цифра
                    if (fldMsgBox.Text.ToLower().Contains("нет"))
                    {
                        logger.UseLogger("INFO", "Stop ordering.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        id = Person.SearchPerson(People, name);
                        if (id == 0)
                        {
                            DialogStatus = Constants.DialogStatus04;
                            logger.UseLogger("INFO", Constants.DialogStatus04, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                            SendSystemMsg(Constants.DialogMsg15);
                            //string name, string email, bool mailing
                        }
                        else
                        {
                            DialogStatus = Constants.DialogStatus06;
                            logger.UseLogger("INFO", "Search email in the database.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
                        logger.UseLogger("INFO", "Start ordering.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
                    switch (fldMsgBox.Text.ToLower())
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
            logger.UseLogger("INFO", Constants.DialogMsg21, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderPayment(email, billInfo);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg22);
            logger.UseLogger("INFO", Constants.DialogMsg22, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderComplited(email, billInfo);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg23);
            logger.UseLogger("INFO", Constants.DialogMsg23, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderDeliveredByCourier(email, billInfo);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                //усилить проверку, так как в отправке словила эксепшн
                var addr = new System.Net.Mail.MailAddress(email);
                logger.UseLogger("ERROR", "Valid email entered.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return addr.Address == email;
            }
            catch
            {
                logger.UseLogger("ERROR", "Error entering mailbox address.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
            logger.UseLogger("INFO", "Order display.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
            logger.UseLogger("INFO", "Show menu.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
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
