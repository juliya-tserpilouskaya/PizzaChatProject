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

        public MainForm()
        {
            InitializeComponent();
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
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Italic); // курсивчик
                fldDialogBox.AppendText("\n\n" + fldMsgBox.Text);
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Regular); // обычный
                //fldMsgBox.Text = String.Empty;
            }
            switch (DialogStatus)
            {
                case Constants.DialogStatus01:
                    name = fldMsgBox.Text;
                    if (name == Constants.AdminName)
                    {
                        DialogStatus = Constants.DialogStatusAdmin01;
                    }
                    else
                    {
                        DialogStatus = Constants.DialogStatus02;
                        SendSystemMsg(Constants.DilogMsg02);
                    }
                    break;
                default: break;
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
