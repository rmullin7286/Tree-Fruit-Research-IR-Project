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

namespace TFREC_IR_app
{
    public partial class settings : Form
    {
        private string port;
        private string interval;
        private string directory;

        public settings()
        {
            InitializeComponent();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            StreamReader load = new StreamReader("settings.dat");
            string buffer;

            //get the directory out of the way
            directory = load.ReadLine();

            //load port
            buffer = load.ReadLine();
            portText.Text = buffer;

            //load temperature units
            buffer = load.ReadLine();
            if (buffer == "c")
                Ccheck.Checked = true;
            else if (buffer == "f")
                Fcheck.Checked = true;
            else
                Kcheck.Checked = true;

            //load ambient settings
            buffer = load.ReadLine();
            if (buffer == "ambient")
                ambientcheck.Checked = true;

            //load object settings
            buffer = load.ReadLine();
            if(buffer == "object")
                objectcheck.Checked = true;

            //load interval settings
            buffer = load.ReadLine();
            intervalText.Text = buffer;

            load.Close();
        }

        private void ambientcheck_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Ccheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Ccheck.Checked == true)
            {
                Fcheck.Checked = false;
                Kcheck.Checked = false;
            }
        }

        private void portText_TextChanged(object sender, EventArgs e)
        {

        }

        private void portSet_Click(object sender, EventArgs e)
        {
            port = portText.Text;
            interval = intervalText.Text;
        }

        private void intervalSet_Click(object sender, EventArgs e)
        {
            interval = intervalText.Text;
            port = portText.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter save = new StreamWriter("settings.dat");
            save.WriteLine(directory);
            save.WriteLine(port);

            if (Ccheck.Checked == true)
                save.WriteLine("c");
            else if (Fcheck.Checked == true)
                save.WriteLine("f");
            else
                save.WriteLine("k");

            if (ambientcheck.Checked == true)
                save.WriteLine("ambient");
            else
                save.WriteLine("!ambient");

            if (objectcheck.Checked == true)
                save.WriteLine("object");
            else
                save.WriteLine("!object");

            save.WriteLine(interval);

            save.Close();
            this.Close();
            
        }

        private void Fcheck_CheckedChanged(object sender, EventArgs e)
        {
            if(Fcheck.Checked == true)
            {
                Kcheck.Checked = false;
                Ccheck.Checked = false;
            }
        }

        private void Kcheck_CheckedChanged(object sender, EventArgs e)
        {
            if(Kcheck.Checked == true)
            {
                Fcheck.Checked = false;
                Ccheck.Checked = false;
            }
        }
    }
}
