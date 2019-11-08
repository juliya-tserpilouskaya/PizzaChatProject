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
            this.btSendMsg.Location = new System.Drawing.Point(194, 418);
            this.btSendMsg.Margin = new System.Windows.Forms.Padding(0);
            this.btSendMsg.Name = "btSendMsg";
            this.btSendMsg.Size = new System.Drawing.Size(75, 23);
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
            this.fldDialogBox.Enabled = false;
            this.fldDialogBox.Location = new System.Drawing.Point(12, 14);
            this.fldDialogBox.Name = "fldDialogBox";
            this.fldDialogBox.Size = new System.Drawing.Size(256, 398);
            this.fldDialogBox.TabIndex = 1;
            this.fldDialogBox.Text = "";
            // 
            // fldMsgBox
            // 
            this.fldMsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fldMsgBox.Location = new System.Drawing.Point(13, 418);
            this.fldMsgBox.Name = "fldMsgBox";
            this.fldMsgBox.Size = new System.Drawing.Size(175, 22);
            this.fldMsgBox.TabIndex = 2;
            this.fldMsgBox.Text = "";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btSendMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(281, 453);
            this.Controls.Add(this.fldMsgBox);
            this.Controls.Add(this.fldDialogBox);
            this.Controls.Add(this.btSendMsg);
            this.Name = "MainForm";
            this.Text = "Chat pizza";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSendMsg;
        private System.Windows.Forms.RichTextBox fldDialogBox;
        private System.Windows.Forms.RichTextBox fldMsgBox;
    }
}

