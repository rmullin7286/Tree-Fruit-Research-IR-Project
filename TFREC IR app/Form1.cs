using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TFREC_IR_app
{
    public partial class Form1 : Form
    {
        enum flags { BACKGROUND_IDLE, BACKGROUND_TEST, BACKGROUND_RUN, BACKGROUND_READ }

        private static string logDirectory;
        private static string port;
        private static string tempType;
        private static string tAmbient;
        private static string tObject;
        private static string interval;
        private static FileStream flog;
        private static flags background;
        private static string read;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            serialPort1.DataReceived += RecievedSerialHandler;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();

            using (StreamReader loadSettings = new StreamReader("settings.dat"))
            {
                logDirectory = loadSettings.ReadLine();
                port = loadSettings.ReadLine();
                tempType = loadSettings.ReadLine();
                tAmbient = loadSettings.ReadLine();
                tObject = loadSettings.ReadLine();
                interval = loadSettings.ReadLine();
                loadSettings.Close();
            }

            background = flags.BACKGROUND_IDLE;
            serialPort1.PortName = port;
        }

        private void setLogDestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog getDirectory = new FolderBrowserDialog();
            getDirectory.Description = "Choose Directory for log files to be stored";
            if (getDirectory.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                logDirectory = getDirectory.SelectedPath;

                //write the updated values to file
                using (FileStream f = new FileStream("settings.dat", FileMode.Create, FileAccess.Write))
                    f.Close();
                using (StreamWriter saveSettings = new StreamWriter("settings.dat"))
                {
                    saveSettings.WriteLine(logDirectory);
                    saveSettings.WriteLine(port);
                    saveSettings.WriteLine(tempType);
                    saveSettings.WriteLine(tAmbient);
                    saveSettings.WriteLine(tObject);
                    saveSettings.WriteLine(interval);
                    saveSettings.Close();
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.ShowDialog(this);
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (flog != null)
                flog.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.ShowDialog();
        }

        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background = flags.BACKGROUND_TEST;
            testConnectionToolStripMenuItem.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //for testing
                if (background == flags.BACKGROUND_TEST)
                {
                    ((BackgroundWorker)sender).ReportProgress(0, "Testing connection...");
                    ((BackgroundWorker)sender).ReportProgress(0, "Sending signal to Arduino...");

                    //send the signal to Arduino
                    if (!serialPort1.IsOpen)
                    {
                        try
                        {
                            serialPort1.Open();
                            serialPort1.Write(new byte[] { 0x00 }, 0, 1); // 0x00 is test
                            ((BackgroundWorker)sender).ReportProgress(0, "Message sent to Arduino.");
                        }
                        catch
                        {
                            ((BackgroundWorker)sender).ReportProgress(0, "Error sending signal to Arduino. Make sure the Arduino is connected" +
                                "and the correct port selected.");
                        }
                    }

                    else
                    {
                        serialPort1.Write(new byte[] { 0x00 }, 0, 1); // 0x00 is test
                        ((BackgroundWorker)sender).ReportProgress(0, "Message sent to Arduino.");
                    }

                    background = flags.BACKGROUND_IDLE;
                }

                //for reading recieved values
                else if (background == flags.BACKGROUND_READ)
                {
                    read = serialPort1.ReadLine();

                    if (read == "S\r")
                        ((BackgroundWorker)sender).ReportProgress(0, "Connection test successful!");
                    else if (read == "F\r")
                        ((BackgroundWorker)sender).ReportProgress(0, "Connection failed.");

                    background = flags.BACKGROUND_IDLE;
                    if (serialPort1.IsOpen)
                        serialPort1.Close();

                    testConnectionToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void RecievedSerialHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            background = flags.BACKGROUND_READ;         
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            listBox1.Items.Add((string)e.UserState);
        }
    }
}
