using System;
using System.IO;
using System.Windows.Forms;

namespace mccm
{
    public partial class ini : Form
    {
        string programpath;
        string mcpath;
        string tempPath;
        string type;
        string versionmc;

        string[] mcversion;
        public ini()
        {
            InitializeComponent();

            string[] dirs = Directory.GetDirectories(@"C:\Users\gianc\AppData\Roaming\.minecraft\versions", "1.??.??");
            foreach (string dir in dirs)
            {
                var dirn = new DirectoryInfo(dir).Name;
                comboBox2.Items.Add(dirn);
            }

            programpath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @".mccm\");

            textBox1.Text = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @".minecraft");
        }

        private void button5_Click(object sender, EventArgs e) //Exit button
        {
            mccmclass.itemsFile(); //Clear ITEMSFILE
            Application.Exit(); //exit program
        }

        //Search MINECRAFT PATH
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)                    
            {
                textBox1.Text = fb.SelectedPath;
                Environment.SpecialFolder root = fb.RootFolder;
            }
        }

        //Default MINECRAFT PATH
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = Path.Combine(Environment.GetFolderPath(
             Environment.SpecialFolder.ApplicationData), @".minecraft");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mcpath = textBox1.Text;
        }

        //Type Minecraft files
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Mod":
                    type = @"\mods\";
                    break;
                case "Resource packs":
                    type = @"\resourcepacks\";
                    break;
                case "Shader":
                    type = @"\shaderpacks\";
                    break;
                default:
                    break;
            }
            reTempPath();
        }

        //RELOAD FILE TYPE
        public void reTempPath()
        {
            if (comboBox1.Text == "Mod")
            {
                comboBox2.Enabled = true;
                if (comboBox2.Text == "null")
                {
                    versionmc = "";
                }
                else
                {
                    versionmc = comboBox2.Text;
                }
            }
            else
            {
                versionmc = "";
                comboBox2.Enabled = false;
            }

            tempPath = $"{mcpath}{type}{versionmc}";
            toolStripStatusLabel1.Text = tempPath;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            reTempPath();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ready();
        }

        private void ready()
        {
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            mccmclass.MoveFilesToMinecraft(tempPath);
            mccmclass.itemsFile();
            Application.Exit();
        }
    }
}
