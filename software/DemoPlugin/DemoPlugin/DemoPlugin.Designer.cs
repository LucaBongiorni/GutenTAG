namespace DemoPlugin
{
    partial class GutenTAGPlugin
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
            this.readBTN = new System.Windows.Forms.Button();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressTB = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.valueTB = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readBTN
            // 
            this.readBTN.Location = new System.Drawing.Point(75, 23);
            this.readBTN.Name = "readBTN";
            this.readBTN.Size = new System.Drawing.Size(75, 23);
            this.readBTN.TabIndex = 0;
            this.readBTN.Text = "Read";
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.readBTN_Click);
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(9, 9);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(60, 13);
            this.addressLabel.TabIndex = 1;
            this.addressLabel.Text = "Block (hex)";
            // 
            // addressTB
            // 
            this.addressTB.Location = new System.Drawing.Point(12, 25);
            this.addressTB.Name = "addressTB";
            this.addressTB.Size = new System.Drawing.Size(57, 20);
            this.addressTB.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Write";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // valueTB
            // 
            this.valueTB.Location = new System.Drawing.Point(156, 25);
            this.valueTB.Name = "valueTB";
            this.valueTB.Size = new System.Drawing.Size(57, 20);
            this.valueTB.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Value (hex)";
            // 
            // GutenTAGPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 64);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valueTB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addressTB);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.readBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GutenTAGPlugin";
            this.Text = "DemoPlugin";
            this.Load += new System.EventHandler(this.DemoPlugin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.MaskedTextBox addressTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox valueTB;
        private System.Windows.Forms.Label label1;
    }
}