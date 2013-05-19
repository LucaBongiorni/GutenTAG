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
    public partial class modifyblock : Form
    {
        public delegate void TAGMemoryModifiedHandler(byte[] TagMem);
        public event TAGMemoryModifiedHandler TAGMemoryModified;

        private byte[] TagMemory;

        public modifyblock(byte[] TagMem)
        {
            InitializeComponent();
            this.TagMemory = TagMem;
            blockTB.Text = "0";
            updateValues(0);
        }

        private void updateValues(int block)
        {
            if ((block >= 0) && (block < 128))
            {
                byte0TB.Text = string.Format("{0:X}", TagMemory[block * 4]);
                byte1TB.Text = string.Format("{0:X}", TagMemory[block * 4 + 1]);
                byte2TB.Text = string.Format("{0:X}", TagMemory[block * 4 + 2]);
                byte3TB.Text = string.Format("{0:X}", TagMemory[block * 4 + 3]);
            }
            else
            {
                byte0TB.Text = "???";
                byte1TB.Text = "???";
                byte2TB.Text = "???";
                byte3TB.Text = "???";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int block = Convert.ToInt32(blockTB.Text, 16);
                if((block < 0) && (block > 127))
                {
                    MessageBox.Show("Error: Block address must be between 0 and 7F.");
                    return;
                }
                TagMemory[block*4]=Convert.ToByte(byte0TB.Text,16);
                TagMemory[block * 4 + 1] = Convert.ToByte(byte1TB.Text, 16);
                TagMemory[block * 4 + 2] = Convert.ToByte(byte2TB.Text, 16);
                TagMemory[block * 4 + 3] = Convert.ToByte(byte3TB.Text, 16);

                if (TAGMemoryModified != null)
                    TAGMemoryModified(TagMemory);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error: numbers must be hexadecimal.");
            }
        }

        private void blockTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int block = Convert.ToInt32(blockTB.Text,16);
                updateValues(block);
            }
            catch
            {

            }
        }
    }
}
