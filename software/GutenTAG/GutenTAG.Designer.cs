namespace GutenTAG
{
    partial class GutenTAG
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.readBTN = new System.Windows.Forms.Button();
            this.writeBTN = new System.Windows.Forms.Button();
            this.loadBTN = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.verifyBTN = new System.Windows.Forms.Button();
            this.directModifyBTN = new System.Windows.Forms.Button();
            this.viewBTN = new System.Windows.Forms.Button();
            this.resetOTPBTN = new System.Windows.Forms.Button();
            this.pluginList = new System.Windows.Forms.ComboBox();
            this.loadPluginBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(174, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(252, 138);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // readBTN
            // 
            this.readBTN.Enabled = false;
            this.readBTN.Location = new System.Drawing.Point(12, 12);
            this.readBTN.Name = "readBTN";
            this.readBTN.Size = new System.Drawing.Size(75, 23);
            this.readBTN.TabIndex = 1;
            this.readBTN.Text = "Read";
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.readBTN_Click);
            // 
            // writeBTN
            // 
            this.writeBTN.Enabled = false;
            this.writeBTN.Location = new System.Drawing.Point(12, 70);
            this.writeBTN.Name = "writeBTN";
            this.writeBTN.Size = new System.Drawing.Size(75, 23);
            this.writeBTN.TabIndex = 2;
            this.writeBTN.Text = "Write";
            this.writeBTN.UseVisualStyleBackColor = true;
            this.writeBTN.Click += new System.EventHandler(this.writeBTN_Click);
            // 
            // loadBTN
            // 
            this.loadBTN.Location = new System.Drawing.Point(93, 12);
            this.loadBTN.Name = "loadBTN";
            this.loadBTN.Size = new System.Drawing.Size(75, 23);
            this.loadBTN.TabIndex = 3;
            this.loadBTN.Text = "Load BIN";
            this.loadBTN.UseVisualStyleBackColor = true;
            this.loadBTN.Click += new System.EventHandler(this.loadBTN_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Save BIN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // verifyBTN
            // 
            this.verifyBTN.Enabled = false;
            this.verifyBTN.Location = new System.Drawing.Point(12, 41);
            this.verifyBTN.Name = "verifyBTN";
            this.verifyBTN.Size = new System.Drawing.Size(75, 23);
            this.verifyBTN.TabIndex = 5;
            this.verifyBTN.Text = "Verify";
            this.verifyBTN.UseVisualStyleBackColor = true;
            this.verifyBTN.Click += new System.EventHandler(this.verifyBTN_Click);
            // 
            // directModifyBTN
            // 
            this.directModifyBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.directModifyBTN.Location = new System.Drawing.Point(93, 70);
            this.directModifyBTN.Name = "directModifyBTN";
            this.directModifyBTN.Size = new System.Drawing.Size(75, 23);
            this.directModifyBTN.TabIndex = 6;
            this.directModifyBTN.Text = "Modify Block";
            this.directModifyBTN.UseVisualStyleBackColor = true;
            this.directModifyBTN.Click += new System.EventHandler(this.directModifyBTN_Click);
            // 
            // viewBTN
            // 
            this.viewBTN.Location = new System.Drawing.Point(93, 99);
            this.viewBTN.Name = "viewBTN";
            this.viewBTN.Size = new System.Drawing.Size(75, 23);
            this.viewBTN.TabIndex = 7;
            this.viewBTN.Text = "View";
            this.viewBTN.UseVisualStyleBackColor = true;
            this.viewBTN.Click += new System.EventHandler(this.viewBTN_Click);
            // 
            // resetOTPBTN
            // 
            this.resetOTPBTN.Enabled = false;
            this.resetOTPBTN.Location = new System.Drawing.Point(12, 99);
            this.resetOTPBTN.Name = "resetOTPBTN";
            this.resetOTPBTN.Size = new System.Drawing.Size(75, 23);
            this.resetOTPBTN.TabIndex = 8;
            this.resetOTPBTN.Text = "Reset OTP";
            this.resetOTPBTN.UseVisualStyleBackColor = true;
            this.resetOTPBTN.Click += new System.EventHandler(this.resetOTPBTN_Click);
            // 
            // pluginList
            // 
            this.pluginList.Location = new System.Drawing.Point(13, 129);
            this.pluginList.Name = "pluginList";
            this.pluginList.Size = new System.Drawing.Size(96, 21);
            this.pluginList.TabIndex = 9;
            // 
            // loadPluginBTN
            // 
            this.loadPluginBTN.Enabled = false;
            this.loadPluginBTN.Location = new System.Drawing.Point(115, 127);
            this.loadPluginBTN.Name = "loadPluginBTN";
            this.loadPluginBTN.Size = new System.Drawing.Size(53, 23);
            this.loadPluginBTN.TabIndex = 10;
            this.loadPluginBTN.Text = "Load";
            this.loadPluginBTN.UseVisualStyleBackColor = true;
            this.loadPluginBTN.Click += new System.EventHandler(this.loadPluginBTN_Click);
            // 
            // GutenTAG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 164);
            this.Controls.Add(this.loadPluginBTN);
            this.Controls.Add(this.pluginList);
            this.Controls.Add(this.resetOTPBTN);
            this.Controls.Add(this.viewBTN);
            this.Controls.Add(this.directModifyBTN);
            this.Controls.Add(this.verifyBTN);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.loadBTN);
            this.Controls.Add(this.writeBTN);
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.richTextBox1);
            this.Name = "GutenTAG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GutenTAG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GutenTAG_FormClosing);
            this.Load += new System.EventHandler(this.GutenTAG_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Button writeBTN;
        private System.Windows.Forms.Button loadBTN;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button verifyBTN;
        private System.Windows.Forms.Button directModifyBTN;
        private System.Windows.Forms.Button viewBTN;
        private System.Windows.Forms.Button resetOTPBTN;
        private System.Windows.Forms.ComboBox pluginList;
        private System.Windows.Forms.Button loadPluginBTN;
    }
}

