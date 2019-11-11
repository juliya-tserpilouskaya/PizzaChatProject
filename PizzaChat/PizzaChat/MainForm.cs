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
        public string DialogStatus=Constants.DialogStatus01;

        Dictionary<byte, Person> People = new Dictionary<byte, Person>();
        Dictionary<byte, ClassLibrary.Menu> MenuPizza = new Dictionary<byte, ClassLibrary.Menu>();

        public MainForm()
        {
            InitializeComponent();
            Constants.CreateDictionary(People, MenuPizza);
            fldDialogBox.AppendText(Constants.DilogMsg01);

            btSendMsg.Click += new EventHandler(btSendMsg_Click);
        }

        public void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void SendSystemMsg(string msg)
        {
            fldDialogBox.AppendText("\n\n" + msg);
        }

        public void btSendMsg_Click(object sender, EventArgs e)
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
                                       
                    SendSystemMsg(Constants.DilogMsg02);
                    ShowMenu();

                    if (name == Constants.AdminName)
                    {
                        DialogStatus = Constants.DialogStatusAdmin01;
                        SendSystemMsg(Constants.DilogMsg04);
                    }
                    else
                    {
                        DialogStatus = Constants.DialogStatus02;
                        // введите че выбрали
                    }

                    fldMsgBox.Text = String.Empty;
                    break;
                default: break;
            }
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


        public event EventHandler SendMsg;

        private void FldMsgBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
