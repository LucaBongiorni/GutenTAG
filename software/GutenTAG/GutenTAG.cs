using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Threading;
using System.IO;
using GutenTAGPlugin;
using System.Reflection;

namespace GutenTAG
{
    public partial class GutenTAG : Form, IGutenTAG
    {
        public byte[] TagMemory = new byte[512];

        private static UsbDevice MyUsbDevice;
        private static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x04D8, 0xFF28);
        private static UsbEndpointReader reader;
        private static UsbEndpointWriter writer;

        public ErrorCode ec = ErrorCode.None;
        public bool isOpen = false;
        public bool wasTagPresent = false;
        public bool runGutenTAGThread;
        private enum GutenTAGTaskEnum { Idle, Read, Write, Verify, ResetOTP };
        private GutenTAGTaskEnum GutenTAGTask;

        Thread GTThread;

        public void writeTagBlock(byte block, byte[] data)
        {
            int bytesWritten;
            int bytesRead;

            ec = writer.Write(new byte[] { 0x04, block, data[0], data[1], data[2], data[3] }, 2000, out bytesWritten);
            if (ec != ErrorCode.None)
                throw new Exception(UsbDevice.LastErrorString);

            byte[] readBuffer = new byte[4];
            ec = reader.Read(readBuffer, 1000, out bytesRead);
            if (bytesRead != 1)
                throw new Exception("Read error. We was expecting 1 bytes. We read " + bytesRead + " bytes!");
            Thread.Sleep(15);
        }

        public byte[] getTagBlock(byte block)
        {
            int bytesWritten;
            int bytesRead;

            ec = writer.Write(new byte[] { 0x03, block }, 2000, out bytesWritten);
            if (ec != ErrorCode.None)
                throw new Exception(UsbDevice.LastErrorString);

            byte[] readBuffer = new byte[4];
            ec = reader.Read(readBuffer, 1000, out bytesRead);
            if (bytesRead != 4)
                throw new Exception("Read error. We was expecting 4 bytes. We read " + bytesRead + " bytes!");

            return readBuffer;
        }

        public byte[] getTagUID()
        {
            int bytesWritten;
            int bytesRead;

            ec = writer.Write(new byte[] { 0x02 }, 2000, out bytesWritten);
            if (ec != ErrorCode.None)
                throw new Exception(UsbDevice.LastErrorString);

            byte[] readBuffer = new byte[8];
            ec = reader.Read(readBuffer, 1000, out bytesRead);
            if (bytesRead != 8)
                throw new Exception("Read error. We was expecting 8 bytes. We read " + bytesRead + " bytes!");

            Array.Reverse(readBuffer);
            return readBuffer;
        }

        public bool isTagPresent()
        {
            int bytesWritten;
            int bytesRead;

            ec = writer.Write(new byte[] { 0x01 }, 2000, out bytesWritten);
            if (ec != ErrorCode.None)
                throw new Exception(UsbDevice.LastErrorString);

            byte[] readBuffer = new byte[8];
            ec = reader.Read(readBuffer, 1000, out bytesRead);
            if (bytesRead != 1)
                throw new Exception("Read error. We was expectin 1 byte. We read " + bytesRead + " bytes!");

            if (readBuffer[0] == 1)
                return true;
            else
                return false;
        }

        public void GutenTAGClose()
        {
            if (reader != null)
                reader.Dispose();
            if (writer != null)
                writer.Dispose();
            if (MyUsbDevice != null)
                if (MyUsbDevice.IsOpen)
                    MyUsbDevice.Close();
            isOpen = false;
        }

        public void GutenTAGOpen()
        {
            MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);
            if (MyUsbDevice == null)
            {
                isOpen = false;
                return;
            }
            IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                wholeUsbDevice.SetConfiguration(1); // Select config #1
                wholeUsbDevice.ClaimInterface(0);   // Claim interface #0.
            }
            reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
            writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);
            isOpen = true;
        }

        public GutenTAG()
        {
            InitializeComponent();
            runGutenTAGThread = true;
        }

        private void GutenTAGThread()
        {
            while (runGutenTAGThread)
            {
                try
                {
                    if (!isOpen)
                    {
                        GutenTAGOpen();
                        if (isOpen)
                            writeRTB(richTextBox1, "GutenTAG Connected!\r\nSearching for TAG...\r\n");
                    }
                    if (isOpen)
                    {
                        switch (GutenTAGTask)
                        {
                            case GutenTAGTaskEnum.Idle:
                                bool nowTagPresent = isTagPresent();
                                if (wasTagPresent)
                                {
                                    if (!nowTagPresent)
                                    {
                                        writeRTB(richTextBox1, "TAG Lost!\r\n");
                                        enableButtons(false);
                                    }
                                }
                                else
                                {
                                    if (nowTagPresent)
                                    {
                                        writeRTB(richTextBox1, "TAG Found!\r\n");
                                        writeRTB(richTextBox1, "TAG UID = 0x" + BitConverter.ToString(getTagUID()).Replace("-", "") + "\r\n");
                                        writeRTB(richTextBox1, "TAG SYS = 0x" + BitConverter.ToString(getTagBlock(255)).Replace("-", "") + "\r\n");
                                        enableButtons(true);
                                    }
                                }
                                wasTagPresent = nowTagPresent;
                                break;

                            case GutenTAGTaskEnum.Read:
                                enableButtons(false);
                                writeRTB(richTextBox1, "Reading TAG...\r\n");
                                for (byte i = 0; i < 128; i++)
                                    Array.Copy(getTagBlock(i), 0, TagMemory, i * 4, 4);
                                writeRTB(richTextBox1, "Done!\r\n");
                                GutenTAGTask = GutenTAGTaskEnum.Idle;
                                enableButtons(true);
                                break;

                            case GutenTAGTaskEnum.Write:
                                enableButtons(false);
                                writeRTB(richTextBox1, "Writing TAG...\r\n");
                                for (byte i = 0; i < 128; i++)
                                {
                                    byte[] buffer = new byte[4];
                                    Array.Copy(TagMemory, i * 4, buffer, 0, 4);
                                    writeTagBlock(i, buffer);
                                }
                                writeRTB(richTextBox1, "Done!\r\n");
                                GutenTAGTask = GutenTAGTaskEnum.Idle;
                                enableButtons(true);
                                break;

                            case GutenTAGTaskEnum.Verify:
                                enableButtons(false);
                                writeRTB(richTextBox1, "Verifying TAG...\r\n");
                                for (byte i = 0; i < 128; i++)
                                {
                                    byte[] block = getTagBlock(i);
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (TagMemory[i * 4 + j] != block[j])
                                        {
                                            GutenTAGTask = GutenTAGTaskEnum.Idle;
                                            break;
                                        }
                                    }
                                    if (GutenTAGTask == GutenTAGTaskEnum.Idle)
                                        break;
                                }
                                if (GutenTAGTask == GutenTAGTaskEnum.Idle)
                                    writeRTB(richTextBox1, "Verify FAIL!\r\n");
                                else
                                    writeRTB(richTextBox1, "Verify OK!\r\n");
                                GutenTAGTask = GutenTAGTaskEnum.Idle;
                                enableButtons(true);
                                break;

                            case GutenTAGTaskEnum.ResetOTP:
                                enableButtons(false);
                                writeRTB(richTextBox1, "OTP Area Reset...\r\n");
                                uint counter = BitConverter.ToUInt32(getTagBlock(0x06), 0);
                                if (MessageBox.Show((counter >> 21).ToString() + " operations left. Are you sure?", "Confirm OTP Erase", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    writeTagBlock(0x06, BitConverter.GetBytes(counter - 0x200000));
                                    for (byte i = 0; i < 6; i++)
                                        writeTagBlock(i, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
                                    writeRTB(richTextBox1, "Done!\r\n");
                                }
                                else
                                {
                                    writeRTB(richTextBox1, "Cancelled.\r\n");
                                }
                                GutenTAGTask = GutenTAGTaskEnum.Idle;
                                enableButtons(true);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    writeRTB(richTextBox1, "ERROR: " + ex.Message + "\r\n");
                    GutenTAGClose();
                    wasTagPresent = false;
                }
            }
        }

        private delegate void enableButtonsDelegate(bool enabled);
        private void enableButtons(bool enabled)
        {
            if ((readBTN.InvokeRequired) || (writeBTN.InvokeRequired))
            {
                enableButtonsDelegate inv = new enableButtonsDelegate(enableButtons);
                readBTN.Invoke(inv, new object[] { enabled });
            }
            else
            {
                readBTN.Enabled = enabled;
                writeBTN.Enabled = enabled;
                verifyBTN.Enabled = enabled;
                resetOTPBTN.Enabled = enabled;
                enableLoadBTN();
            }
        }

        private delegate void writeRTBDelegate(RichTextBox target, string value);
        private void writeRTB(RichTextBox target, string value)
        {
            if (target.InvokeRequired)
            {
                writeRTBDelegate inv = new writeRTBDelegate(writeRTB);
                target.Invoke(inv, new object[] { target, value });
            }
            else
            {
                target.AppendText(value);
                target.ScrollToCaret();
            }
        }

        private void GutenTAGThreadStart()
        {
            if (GTThread != null)
                if (GTThread.IsAlive)
                    return;
            GTThread = new Thread(new ThreadStart(GutenTAGThread));
            GTThread.Start();
        }

        private void GutenTAG_Load(object sender, EventArgs e)
        {
            GutenTAGThreadStart();
            writeRTB(richTextBox1, "GutenTAG V:2.0\r\n");
            writeRTB(richTextBox1, "Searching for GutenTAG...\r\n");
            getPlugins();
        }

        private void getPlugins()
        {
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath));
            FileInfo[] rgFiles = di.GetFiles("*.dll");

            foreach (FileInfo fi in rgFiles)
            {
                try
                {
                    string name = fi.Name.Split('.')[0];
                    Assembly a = Assembly.Load(name);
                    Type t = null;
                    if (a != null)
                        t = a.GetType(name + ".GutenTAGPlugin");
                    if (t != null)
                    {
                        IGutenTAGPlugin p = (IGutenTAGPlugin)Activator.CreateInstance(t);
                        pluginList.Items.Add(p);
                        p.GutenTAGHost = this;
                        pluginList.SelectedIndex = 0;
                        enableLoadBTN();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(fi.Name + ": " + ex.Message);
                }
            }
        }

        private void enableLoadBTN()
        {
            if (pluginList.Items != null)
                if (pluginList.Items.Count != 0)
                    loadPluginBTN.Enabled = readBTN.Enabled;
        }

        private void GutenTAG_FormClosing(object sender, FormClosingEventArgs e)
        {
            runGutenTAGThread = false;
            GTThread.Join();
            GutenTAGClose();
        }

        private void readBTN_Click(object sender, EventArgs e)
        {
            if (GutenTAGTask != GutenTAGTaskEnum.Idle)
                return;
            GutenTAGTask = GutenTAGTaskEnum.Read;
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Binary Files(*.bin)|*.bin";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(fd.FileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(TagMemory);
                writer.Close();
                writeRTB(richTextBox1, "File Saved.\r\n");
            }
        }

        private void writeBTN_Click(object sender, EventArgs e)
        {
            if (GutenTAGTask != GutenTAGTaskEnum.Idle)
                return;
            GutenTAGTask = GutenTAGTaskEnum.Write;
        }

        private void verifyBTN_Click(object sender, EventArgs e)
        {
            if (GutenTAGTask != GutenTAGTaskEnum.Idle)
                return;
            GutenTAGTask = GutenTAGTaskEnum.Verify;
        }

        private void loadBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Binary Files(*.bin)|*.bin";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(fd.FileName, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                reader.Read(TagMemory, 0, TagMemory.Length);
                reader.Close();
                writeRTB(richTextBox1, "File opened.\r\n");
            }
        }

        private void directModifyBTN_Click(object sender, EventArgs e)
        {
            modifyblock mb = new modifyblock(TagMemory);
            mb.TAGMemoryModified += new modifyblock.TAGMemoryModifiedHandler(mb_TAGMemoryModified);
            mb.ShowDialog();
        }

        void mb_TAGMemoryModified(byte[] TagMem)
        {
            Array.Copy(TagMem, TagMemory, TagMem.Length);
        }

        private void viewBTN_Click(object sender, EventArgs e)
        {
            view v = new view(TagMemory);
            v.ShowDialog();
        }

        private void resetOTPBTN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ARE YOU SURE TO RESET OTP AREA?", "RESET OTP?", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (GutenTAGTask != GutenTAGTaskEnum.Idle)
                return;
            GutenTAGTask = GutenTAGTaskEnum.ResetOTP;
        }

        private void loadPluginBTN_Click(object sender, EventArgs e)
        {
            IGutenTAGPlugin plugin;
            plugin = (IGutenTAGPlugin)pluginList.Items[pluginList.SelectedIndex];
            runGutenTAGThread = false;
            GTThread.Join();
            plugin.Show();
        }

        void plugin_WriteTAG(byte address, byte[] block)
        {
            writeTagBlock(address, block);
        }

        byte[] plugin_ReadTAG(byte address)
        {
            return getTagBlock(address);
        }

        byte[] plugin_GetUIDTAG()
        {
            return getTagUID();
        }

        public void closePlugin()
        {
            runGutenTAGThread = true;
            GutenTAGThreadStart();
            pluginList.Items.Clear();
            getPlugins();
        }
    }
}
