namespace PizzaChat
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btSendMsg = new System.Windows.Forms.Button();
            this.fldDialogBox = new System.Windows.Forms.RichTextBox();
            this.fldMsgBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btSendMsg
            // 
            this.btSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSendMsg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btSendMsg.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btSendMsg.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btSendMsg.FlatAppearance.BorderSize = 0;
            this.btSendMsg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btSendMsg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btSendMsg.Font = new System.Drawing.Font("Microsoft Yi Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSendMsg.Location = new System.Drawing.Point(259, 514);
            this.btSendMsg.Margin = new System.Windows.Forms.Padding(0);
            this.btSendMsg.Name = "btSendMsg";
            this.btSendMsg.Size = new System.Drawing.Size(100, 28);
            this.btSendMsg.TabIndex = 0;
            this.btSendMsg.Text = "Отпавить";
            this.btSendMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btSendMsg.UseVisualStyleBackColor = false;
            // 
            // fldDialogBox
            // 
            this.fldDialogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fldDialogBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fldDialogBox.HideSelection = false;
            this.fldDialogBox.Location = new System.Drawing.Point(16, 17);
            this.fldDialogBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fldDialogBox.Name = "fldDialogBox";
            this.fldDialogBox.ReadOnly = true;
            this.fldDialogBox.Size = new System.Drawing.Size(340, 489);
            this.fldDialogBox.TabIndex = 1;
            this.fldDialogBox.Text = "";
            // 
            // fldMsgBox
            // 
            this.fldMsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fldMsgBox.Location = new System.Drawing.Point(17, 514);
            this.fldMsgBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fldMsgBox.Name = "fldMsgBox";
            this.fldMsgBox.Size = new System.Drawing.Size(232, 26);
            this.fldMsgBox.TabIndex = 2;
            this.fldMsgBox.Text = "";
            this.fldMsgBox.TextChanged += new System.EventHandler(this.FldMsgBox_TextChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btSendMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(375, 558);
            this.Controls.Add(this.fldMsgBox);
            this.Controls.Add(this.fldDialogBox);
            this.Controls.Add(this.btSendMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Hamster pizza chat";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RichTextBox fldDialogBox;
        public System.Windows.Forms.Button btSendMsg;
        public System.Windows.Forms.RichTextBox fldMsgBox;
    }
}

