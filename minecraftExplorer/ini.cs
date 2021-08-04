using System;
using System.IO;
using System.Windows.Forms;

namespace mcex
{
    public partial class ini : Form
    {
        string programpath;
        string mcpath;
        string tempPath;
        string type;
        string versionmc;

        public ini()
        {
            InitializeComponent();

            programpath = Path.Combine(Environment.GetFolderPath(
             Environment.SpecialFolder.ApplicationData), @".mcex\");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mcexclass.itemsFile();
            Application.Exit();
        }

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

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = Path.Combine(Environment.GetFolderPath(
             Environment.SpecialFolder.ApplicationData), @".minecraft");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mcpath = textBox1.Text;
        }

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
                case "Plugin":
                    type = @"\plugins\";
                    break;
                default:
                    break;
            }
            reTempPath();
        }

        public void reTempPath()
        {
            if (comboBox1.Text == "Mod")
            {
                comboBox2.Enabled = true;
                if (comboBox2.Text == "n/a")
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
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
            mcexclass.MoveFilesToMinecraft(tempPath);
            mcexclass.itemsFile();
            MessageBox.Show("Completed");
            Application.Exit();
        }

        private void ini_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
