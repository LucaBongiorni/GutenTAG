using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GutenTAGPlugin;

namespace DemoPlugin
{
    public partial class GutenTAGPlugin : Form, IGutenTAGPlugin
    {
        #region CODICE NECESSARIO PER ESSERE RICONOSCIUTO DAL PROGRAMMA GUTENTAG
        private IGutenTAG PluginGutenTAGHost;
        public IGutenTAG GutenTAGHost
        {
            get { return PluginGutenTAGHost; }
            set { PluginGutenTAGHost = value; }
        }
        public override string ToString()
        {
            return "DemoPlugin";
        }
        #endregion

        public GutenTAGPlugin()
        {
            InitializeComponent();
        }

        private void DemoPlugin_Load(object sender, EventArgs e)
        {

        }

        private void readBTN_Click(object sender, EventArgs e)
        {
            //SE IL TAG E' PRESENTE E SELEZIONATO
            if (GutenTAGHost.isTagPresent()) {
                //PRENDE INDIRIZZO BLOCCO
                byte address = (byte)Convert.ToInt32(addressTB.Text,16);
                //LEGGE BLOCCO
                byte[] block = GutenTAGHost.getTagBlock(address);
                //VISUALIZZA INDIRIZZO
                valueTB.Text = helpers.ByteArrayToHexString(block);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SE IL TAG E' PRESENTE E SELEZIONATO
            if (GutenTAGHost.isTagPresent())
            {
                //PRENDE INDIRIZZO BLOCCO
                byte address = (byte)Convert.ToInt32(addressTB.Text, 16);
                //PRENDE DATI BLOCCK
                byte[] block = helpers.HexStringToByteArray(valueTB.Text);
                //SCRIVE BLOCCO
                GutenTAGHost.writeTagBlock(address, block);
            }
        }
    }
}
