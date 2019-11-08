using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace PizzaChat
{
    public interface IMainForm
    {
        string DialogBox { get; }
        string MsgBox { get; set; }
        //void SendMsg(string Msg);
        //event EventHandler SendMsg;
    }
    public partial class MainForm : Form, IMainForm
    {
        public string[] ArrMsg;

        public MainForm()
        {
            InitializeComponent();
            btSendMsg.Click += new EventHandler(btSendMsg_Click);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        void btSendMsg_Click(object sender, EventArgs e)
        {
            if (fldMsgBox.Text.Length > 0)
            {
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Italic); // жирный
                fldDialogBox.AppendText( fldMsgBox.Text + "\n\n");
                fldDialogBox.SelectionFont = new Font(fldDialogBox.Font.FontFamily, this.Font.Size, FontStyle.Regular); // обычный
                fldMsgBox.Text = String.Empty;
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
    }
}
