using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GutenTAG
{
    public partial class view : Form
    {
        byte[] TagMemory;

        public view(byte[] TagMem)
        {
            InitializeComponent();
            TagMemory = TagMem;
        }

        private void view_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < TagMemory.Length / 8; i++)
            {
                string row = (i * 2).ToString("X").PadLeft(2,'0');
                string b1 = BitConverter.ToString(TagMemory, i * 8, 4).Replace('-', ' ');
                string b2 = BitConverter.ToString(TagMemory, i * 8 + 4, 4).Replace('-', ' ');
                richTextBox1.AppendText(row + ": " + b1 + "  " + b2 + "\r\n");
                richTextBox1.Select(0, 1);
                richTextBox1.Select(0, 0);
                richTextBox1.ScrollToCaret();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
