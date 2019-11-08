using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaChat
{
    public interface IMainForm
    {
        string DialogBox { get; }
        string MsgBox { get; set; }
        //void SendMsg(string Msg);
        event EventHandler SendMsg;
    }
    public partial class MainForm : Form, IMainForm
    {

        public MainForm()
        {
            InitializeComponent();
            btSendMsg.Click += new EventHandler(btSendMsg_Click);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fldDialogBox.Text += fldMsgBox.Text;
            fldMsgBox.Text = String.Empty;

        }

        void btSendMsg_Click(object sender, EventArgs e)
        {
            if (fldMsgBox.Text.Length > 0)
            {
                fldDialogBox.Text = fldDialogBox.Text + "\n" + fldMsgBox.Text;
                fldMsgBox.Text = String.Empty;
            }
            //this.txtBlock.Background = new SolidColorBrush(Colors.LightGray);
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
    }
}
