using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.ComponentModel;
using Microsoft.Win32;

namespace mccmInstaller
{
    public partial class iniins : Form
    {
        private WebClient download1;
        string mccmpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".mccm\");

        public iniins()
        {
            InitializeComponent();
            textBox1.Text = mccmpath;
            download1 = new WebClient();
            download1.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            install();
        }

        private void install()
        {
            button2.Enabled = false;
            button3.Enabled = false;

            if (!Directory.Exists(mccmpath))
            {
                Directory.CreateDirectory(mccmpath);
            }
            registerkey();
            Download();
        }

        private void registerkey()
        {
            const string userRoot = "HKEY_CLASSES_ROOT";

            const string subkey0 = "*\\shell\\mccm";
            const string subkey1 = "*\\shell\\mccm\\command";
            const string keyName0 = userRoot + "\\" + subkey0;
            const string keyName1 = userRoot + "\\" + subkey1;

            try
            {
                Registry.SetValue(keyName0, "", "Add to Minecraft");
                Registry.SetValue(keyName1, "", "\"" + mccmpath + "mccm.exe\" \"additem\" \"%1\"");
            }
            catch (Exception e)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result;
                string message = "Error creating key in" + userRoot + "\n" +
                                 "Exception" + e;
                string caption = "";

                result = MessageBox.Show(message, caption, buttons, icon);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void Download()
        {
            string url = "https://raw.githubusercontent.com/grpzz/mccm/master/mccm/bin/mccm.exe";
            string path = mccmpath + "mccm.exe";
            download1.DownloadFileAsync(new Uri(url), path);
        }

        private void cargado2(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
               //Process.Start("\"" + mcexpath + "mcex.exe\" \"install\" \"%1\"");

                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result;
                string message = "Installation finished.\n" +
                                 "If you find a bug, report it at-- > https://github.com/grpzz/mccm/discussions";
                string caption = "";
                result = MessageBox.Show(message, caption, buttons, icon);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch 
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result;
                string message = "Installation error.\n" +
                                 "Report it at-- > https://github.com/grpzz/mccm/discussions";
                string caption = "";

                result = MessageBox.Show(message, caption, buttons, icon);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
