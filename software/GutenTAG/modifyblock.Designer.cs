namespace GutenTAG
{
    partial class modifyblock
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
            this.button1 = new System.Windows.Forms.Button();
            this.blockTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.byte3TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.byte2TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.byte1TB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.byte0TB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // blockTB
            // 
            this.blockTB.Location = new System.Drawing.Point(12, 25);
            this.blockTB.Name = "blockTB";
            this.blockTB.Size = new System.Drawing.Size(46, 20);
            this.blockTB.TabIndex = 1;
            this.blockTB.TextChanged += new System.EventHandler(this.blockTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Block";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Byte 3";
            // 
            // byte3TB
            // 
            this.byte3TB.Location = new System.Drawing.Point(220, 25);
            this.byte3TB.Name = "byte3TB";
            this.byte3TB.Size = new System.Drawing.Size(46, 20);
            this.byte3TB.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Byte 2";
            // 
            // byte2TB
            // 
            this.byte2TB.Location = new System.Drawing.Point(168, 25);
            this.byte2TB.Name = "byte2TB";
            this.byte2TB.Size = new System.Drawing.Size(46, 20);
            this.byte2TB.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Byte 1";
            // 
            // byte1TB
            // 
            this.byte1TB.Location = new System.Drawing.Point(116, 25);
            this.byte1TB.Name = "byte1TB";
            this.byte1TB.Size = new System.Drawing.Size(46, 20);
            this.byte1TB.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Byte 0";
            // 
            // byte0TB
            // 
            this.byte0TB.Location = new System.Drawing.Point(64, 25);
            this.byte0TB.Name = "byte0TB";
            this.byte0TB.Size = new System.Drawing.Size(46, 20);
            this.byte0TB.TabIndex = 2;
            // 
            // modifyblock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 81);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.byte0TB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.byte1TB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.byte2TB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.byte3TB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blockTB);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "modifyblock";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify Block";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox blockTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox byte3TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox byte2TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox byte1TB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox byte0TB;
    }
}