using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace mcex
{
    public partial class options : Form
    {
        private WebClient download1;
        private WebClient download2;

        string actuallyversion;
        string lastversion = "";

        public options()
        {
            InitializeComponent();
            download1.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado1);
            download2.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado2);
        }

        private void Update()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            actuallyversion = fvi.FileVersion;

            string url = "https://raw.githubusercontent.com/grpzz/.mcex/master/mcex/version";
            string path = "lastversion";
            download1.DownloadFileAsync(new Uri(url), path);
        }

        private void cargado1(object sender, AsyncCompletedEventArgs e)
        {
            StreamReader file = new System.IO.StreamReader("lastversion");
            FileInfo fi = new FileInfo(lastversion);

            if(lastversion == actuallyversion)
            {
                MessageBox.Show("You have the latest version");
            }
            else
            {
                MessageBox.Show("You don't have the latest version :(, it will download now :D");
                download();
            }
        }

        private void download()
        {
            string url = "https://raw.githubusercontent.com/grpzz/.mcex/master/mcexInstaller/bin/mcexInstaller.exe";
            string path = "mcexInstaller.exe";
            download1.DownloadFileAsync(new Uri(url), path);
        }

        private void cargado2(object sender, AsyncCompletedEventArgs e)
        {
            Process.Start("mcexInstaller.exe");
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Update();
            button1.Enabled = false;
        }

        private void options_Load(object sender, EventArgs e)
        {

        }
    }
}
