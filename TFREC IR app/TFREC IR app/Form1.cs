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
        enum flags { BACKGROUND_IDLE, BACKGROUND_TEST, BACKGROUND_RUN, BACKGROUND_READ , BACKGROUND_WAIT }

        private static string logDirectory;
        private static string port;
        private static string tempType;
        private static string tAmbient;
        private static string tObject;
        private static string interval;
        private static flags background;
        private static string read;
        private static string write;
        private static string[] numbers = new string[2];
        private static int[] tempInt = new int[2];
        private static float[] temps = new float[2];
        private static string currentTimeString;
        private static string fileName;
        private static DayOfWeek prevDay;
        private static DayOfWeek currentDay;
        private static bool dayPassed;

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
            prevDay = DateTime.Now.DayOfWeek;

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

            timer1.Interval = (Convert.ToInt32(interval) * 60000);
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

            if (serialPort1.IsOpen)
                serialPort1.Close() ;
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
            help help = new help();
            help.ShowDialog();
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
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                    ((BackgroundWorker)sender).ReportProgress(0, "Sending signal to Arduino...");
                    listBox1.TopIndex = listBox1.Items.Count - 1;

                    //send the signal to Arduino
                    if (!serialPort1.IsOpen)
                    {
                        try
                        {
                            serialPort1.Open();
                            serialPort1.Write(new byte[] { 0x00 }, 0, 1); // 0x00 is test
                            ((BackgroundWorker)sender).ReportProgress(0, "Message sent to Arduino.");
                            listBox1.TopIndex = listBox1.Items.Count - 1;
                        }
                        catch
                        {
                            ((BackgroundWorker)sender).ReportProgress(0, "Error sending signal to Arduino. Make sure the Arduino is connected" +
                                "and the correct port selected.");
                            listBox1.TopIndex = listBox1.Items.Count - 1;
                        }
                    }

                    else
                    {
                        serialPort1.Write(new byte[] { 0x00 }, 0, 1); // 0x00 is test
                        ((BackgroundWorker)sender).ReportProgress(0, "Message sent to Arduino.");
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                    }

                    background = flags.BACKGROUND_IDLE;
                }

                //for running the main program
                else if (background == flags.BACKGROUND_RUN)
                {
                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.Open();
                    }

                    serialPort1.Write(new byte[] { 0x01 }, 0, 1); // 0x01 is run
                    ((BackgroundWorker)sender).ReportProgress(0, "Initilializing Arduino");
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                    background = flags.BACKGROUND_IDLE;
                }

                //for reading recieved values
                else if (background == flags.BACKGROUND_READ)
                {
                    read = serialPort1.ReadLine();

                    //catches for test
                    if (read == "S\r")
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Connection test successful!");
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                        testConnectionToolStripMenuItem.Enabled = true;
                    }
                    else if (read == "F\r")
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Connection failed.");
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                        testConnectionToolStripMenuItem.Enabled = true;
                    }

                    else if (read == "timeout error.\r")
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "timeout error.");
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                    }

                    //this is for if the user is running the program
                    else
                    {
                        numbers = read.Split(new char[] { ',', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        tempInt[0] = System.Convert.ToInt32(numbers[0]);
                        tempInt[1] = System.Convert.ToInt32(numbers[1]);

                        temps[0] = (float)tempInt[0] / 100.0f;
                        temps[1] = (float)tempInt[0] / 100.0f;

                        currentTimeString = DateTime.Now.ToShortTimeString();

                        write = currentTimeString + String.Format(", {0:0.00}, {0:0.00}", temps[0], temps[1]); // formats as Date, ambient, object 

                        ((BackgroundWorker)sender).ReportProgress(0, write);
                        listBox1.TopIndex = listBox1.Items.Count - 1;

                        fileName = String.Format("{0}-{1}-{2}.csv", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);

                        //check and see if a day has passed. If it has, create a new file
                        currentDay = DateTime.Now.DayOfWeek;

                        if (prevDay == DayOfWeek.Saturday)
                            dayPassed = (currentDay == DayOfWeek.Sunday);
                        else
                            dayPassed = ((int)currentDay > (int)prevDay);

                        prevDay = currentDay;


                        if (File.Exists(logDirectory + "\\" + fileName) && !dayPassed)
                        {
                            using (StreamWriter append = File.AppendText(logDirectory + "\\" + fileName))
                            {
                                append.WriteLine(write);
                                append.Close();

                                ((BackgroundWorker)sender).ReportProgress(0, "logged temp to file.");
                                listBox1.TopIndex = listBox1.Items.Count - 1;
                            }
                        }

                        else
                        {
                            using (StreamWriter create = new StreamWriter(File.Open(logDirectory + "\\" + fileName, System.IO.FileMode.Create)))
                            {
                                create.WriteLine("HH::MM AM/PM, Ambient, Object");
                                create.WriteLine(write);
                                create.Close();

                                ((BackgroundWorker)sender).ReportProgress(0, "Created new log file.");
                                listBox1.TopIndex = listBox1.Items.Count - 1;
                            }
                        }


                        background = flags.BACKGROUND_WAIT;
                    }

                    background = flags.BACKGROUND_IDLE;
                    if (serialPort1.IsOpen)
                        serialPort1.Close();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void runProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            background = flags.BACKGROUND_RUN;
            button1.Visible = true;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            background = flags.BACKGROUND_IDLE;
            button1.Visible = false;

            timer1.Stop();

            serialPort1.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            background = flags.BACKGROUND_RUN;
            timer1.Stop();
            timer1.Start();
        }
    }
}
