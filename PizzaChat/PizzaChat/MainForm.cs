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
            if (fldMsgBox.Text.Length > 0 || fldMsgBox.Text!=String.Empty)
            {
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Italic);
                fldDialogBox.AppendText("\n\n" + fldMsgBox.Text);
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Regular);

                switch (DialogStatus)
                {
                    case Constants.DialogStatus01:
                        SendSystemMsg(Constants.DialogMsg02);
                        ShowMenu();
                        name = fldMsgBox.Text;

                        if (name == Constants.AdminName)
                        {
                            DialogStatus = Constants.DialogStatusAdmin01;
                            SendSystemMsg(Constants.DialogMsg04);
                            logger.UseLogger("INFO", "Admin start administrate...", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                             DialogStatus = Constants.DialogStatus02;
                             SendSystemMsg(Constants.DialogMsg13);
                             logger.UseLogger("INFO", "Be our guest! Put our service to the test!", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());

                        }

                        break;
                    case Constants.DialogStatusAdmin01:
                        int variant;
                        if (!int.TryParse(fldMsgBox.Text, out variant))
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                            break;
                        }
                        if (Convert.ToInt32(fldMsgBox.Text) > 0 && Convert.ToInt32(fldMsgBox.Text) <= Constants.AmountAdminVariants)
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
                        if (InputCheckAdmin(fldMsgBox.Text) == true)
                        {
                            ClassLibrary.Menu.CreatePizza(MenuPizza, fldMsgBox.Text);
                            SendSystemMsg(Constants.DialogMsg08);
                            SendSystemMsg(Constants.DialogMsg04);
                            DialogStatus = Constants.DialogStatusAdmin01;
                            logger.UseLogger("INFO", Constants.DialogStatusAdmin02 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                        }
                        break;
                    case Constants.DialogStatusAdmin03:
                        if (InputCheckAdmin(fldMsgBox.Text) == true)
                        {
                            ClassLibrary.Menu.UpdateMenu(MenuPizza, fldMsgBox.Text);
                            SendSystemMsg(Constants.DialogMsg12);
                            SendSystemMsg(Constants.DialogMsg04);
                            DialogStatus = Constants.DialogStatusAdmin01;
                            logger.UseLogger("INFO", Constants.DialogStatusAdmin03 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                        }
                        break;
                    case Constants.DialogStatusAdmin04:
                        if (InputCheckAdmin(fldMsgBox.Text) == true)
                        {

                            ClassLibrary.Menu.DeletePizza(MenuPizza, Convert.ToByte(fldMsgBox.Text));
                            SendSystemMsg(Constants.DialogMsg10);
                            SendSystemMsg(Constants.DialogMsg04);
                            DialogStatus = Constants.DialogStatusAdmin01;
                            logger.UseLogger("INFO", Constants.DialogStatusAdmin04 + "ended.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                            logger.UseLogger("WARN", "The answer was entered incorrectly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        break;
                    case Constants.DialogStatus02:
                        if (InputCheck(fldMsgBox.Text)==true) 
                        {
                            ClassLibrary.Bill.AddPizza(Order, MenuPizza, fldMsgBox.Text);
                            SendSystemMsg(Constants.DialogMsg14);
                            DialogStatus = Constants.DialogStatus03;
                            logger.UseLogger("INFO", "Menu list updated.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                            logger.UseLogger("WARN", "The answer was entered incorrectly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }                    
                        break;
                    case Constants.DialogStatus03:
                        if (fldMsgBox.Text.ToLower() != "нет" || fldMsgBox.Text.ToLower() != "да")
                        {
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
                                    fldMsgBox.Text = String.Empty;
                                    btSendMsg.Enabled = false;
                                    fldMsgBox.Enabled = false;
                                    SendSystemMsg(Constants.DialogMsg19);
                                    ShowOrder();
                                    email = Person.SearchPersonEmail(People, id);
                                    logger.UseLogger("INFO", "Search email in the database.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                    SendMailing(email);
                                }
                            }
                            else
                            {
                                DialogStatus = Constants.DialogStatus02;
                                SendSystemMsg(Constants.DialogMsg13);
                                logger.UseLogger("INFO", "Start ordering.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                            }
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                            logger.UseLogger("WARN", "The answer was entered incorrectly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        break;
                    case Constants.DialogStatus04:
                        bool correctEmail = IsValidEmail(fldMsgBox.Text);
                        if (correctEmail == false)
                        {
                            SendSystemMsg(Constants.DialogMsg16);
                            logger.UseLogger("WARN", "E-mail was entered incorrectly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        else
                        {
                            email = fldMsgBox.Text;
                            DialogStatus = Constants.DialogStatus05;
                            SendSystemMsg(Constants.DialogMsg17);
                        }
                        break;
                    case Constants.DialogStatus05:
                        if (fldMsgBox.Text.ToLower() != "да" || fldMsgBox.Text.ToLower() != "нет")
                        {
                            switch (fldMsgBox.Text.ToLower())
                            {
                                case "нет":
                                    mailing = false;
                                    logger.UseLogger("INFO", $"The user with the email {email} unsubscribed from the newsletter.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                    break;
                                case "да":
                                    mailing = true;
                                    logger.UseLogger("INFO", $"The user with the email {email} subscribed to the newsletter.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                                    break;
                                default:
                                    break;
                            }
                            fldMsgBox.Text = String.Empty;
                            fldMsgBox.Enabled = false;
                            btSendMsg.Enabled = false;
                            Person.CreateNewPerson(People, name, email, mailing);
                            SendSystemMsg(Constants.DialogMsg18);
                            logger.UseLogger("INFO", "User record created", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                            SendSystemMsg(Constants.DialogMsg19);
                            DialogStatus = Constants.DialogStatus06;
                            ShowOrder();
                            SendMailing(email);
                        }
                        else
                        {
                            SendSystemMsg(Constants.DialogMsg25);
                            logger.UseLogger("WARN", "E-mail was entered incorrectly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        }
                        break;
                    case Constants.DialogStatus06:
                        SendSystemMsg(Constants.DialogMsg26);
                        logger.UseLogger("INFO", "Order completed.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                SendSystemMsg(Constants.DialogMsg25);
                logger.UseLogger("WARN", "User entered an empty string.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }
            fldMsgBox.Text = String.Empty;
        }

        public void SendMailing(string email)
        {
            SendSystemMsg(Constants.DialogMsg21);
            logger.UseLogger("INFO", Constants.DialogMsg21, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderPayment(email, billInfo, logger);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg22);
            logger.UseLogger("INFO", Constants.DialogMsg22, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderComplited(email, billInfo, logger);
            PauseMaker(pauseTime_ms);

            SendSystemMsg(Constants.DialogMsg23);
            logger.UseLogger("INFO", Constants.DialogMsg23, Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            Email.EmailOrderDeliveredByCourier(email, billInfo, logger);
        }
        bool IsValidEmail(string email)
        {
            try
            {
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
            logger.UseLogger("INFO", "Order displayed. Contains:" + billInfo + "Cost:" + sumOrder.ToString(), Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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
            logger.UseLogger("INFO", "The menu was shown on the screen.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
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

        private void PauseMaker(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private bool InputCheck (string msg)
        {
            bool total=false;
            byte Iid;
            int Icount;
            string[] arr = msg.Split(new char[] { ';' });

            if (arr.Length!=3) 
            {
                logger.UseLogger("INFO", "Valid number of values entered.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else 
            {
                logger.UseLogger("WARN", "Incorrect number of values entered.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }
            

            if (!byte.TryParse(arr[0], out Iid))
            {
                logger.UseLogger("INFO", "The pizza number entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else
            {
                logger.UseLogger("WARN", "The pizza number entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }

            if (!int.TryParse(arr[2], out Icount))
            {
                logger.UseLogger("INFO", "The number of pizzas entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else
            {
                logger.UseLogger("WARN", "The number of pizzas entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }

            Iid = Convert.ToByte(arr[0]);

            if (!ClassLibrary.Menu.GetPizza(MenuPizza, Iid))
            {
                logger.UseLogger("INFO", "Pizza ID entered correctly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else
            {
                logger.UseLogger("WARN", "The entered pizza ID does not exist.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }

            if (arr[1].ToLower() != "да" && arr[1].ToLower() != "нет")
            {
                logger.UseLogger("INFO", "The answer to the request for double cheese is entered correctly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else
            {
                logger.UseLogger("WARN", "The response to the double cheese request is incorrect.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }

            Icount = Convert.ToInt32(arr[2]);

            if (Icount < 1)
            {
                logger.UseLogger("INFO", "The number of pizzas entered is correct.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                return total;
            }
            else
            {
                logger.UseLogger("WARN", "The number of pizzas entered is incorrect.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            }

            total = true;
            logger.UseLogger("INFO", "All information on the iteration of the order is entered correctly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            return total;
        }

        private bool InputCheckAdmin(string msg)
        {
            bool total = false;
            int Icount;
            byte Iid;

            switch (DialogStatus)
            {
                case Constants.DialogStatusAdmin04:
                    if (!byte.TryParse(fldMsgBox.Text, out Iid))
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the id of the pizza to delete in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the pizza id to delete in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    Iid = Convert.ToByte(msg);

                    if (ClassLibrary.Menu.GetPizza(MenuPizza, Convert.ToByte(fldMsgBox.Text)) == false)
                    {
                        logger.UseLogger("DEBUG", "The administrator correctly entered the id pizza to delete.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator incorrectly entered the id of the pizza to delete.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }
                    break;
                case Constants.DialogStatusAdmin02:
                    string[] arr = msg.Split(new char[] { ';' });

                    if (arr.Length != 3)
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the correct number of values.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the wrong number of values.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    if (!int.TryParse(arr[2], out Icount))
                    {
                        logger.UseLogger("DEBUG", "The pizza price is entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "Pizza price is entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    if (Convert.ToInt32(arr[2]) < 1)
                    {
                        logger.UseLogger("DEBUG", "The pizza price is entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "Pizza price is entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }
                    break;
                case Constants.DialogStatusAdmin03:
                    string[] arr03 = msg.Split(new char[] { ';' });

                    if (arr03.Length != 4)
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the correct number of values.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the wrong number of values.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    if (!byte.TryParse(arr03[0], out Iid))
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the pizza number in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator entered the pizza number in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    Iid = Convert.ToByte(arr03[0]);

                    if (ClassLibrary.Menu.GetPizza(MenuPizza, Iid) == false)
                    {
                        logger.UseLogger("DEBUG", "The administrator correctly entered the id pizza to change.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "The administrator incorrectly entered the id of the pizza to change.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    if (!int.TryParse(arr03[3], out Icount))
                    {
                        logger.UseLogger("DEBUG", "The pizza price is entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "Pizza price is entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }

                    if (Convert.ToInt32(arr03[3]) < 1)
                    {
                        logger.UseLogger("DEBUG", "The pizza price is entered in the correct format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                        return total;
                    }
                    else
                    {
                        logger.UseLogger("DEBUG", "Pizza price is entered in the wrong format.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    }
                    break;
                default:
                    logger.UseLogger("ERROR", "Programmers call the administrator to testify how he managed to get into this part of the code.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
                    break;
            }
            
            total = true;
            logger.UseLogger("DEBUG", "All information on the iteration of the order is entered correctly.", Thread.GetDomainID().ToString(), GetCurrentMethod().ToString());
            return total;
        }

        private void FldMsgBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
