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
        string mcexpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".mcex\");

        public options()
        {
            InitializeComponent();

            download1 = new WebClient();
            download2 = new WebClient();
            download1.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado1);
            download2.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado2);
        }

        private new void Update()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            actuallyversion = fvi.FileVersion;

            label2.Text = actuallyversion;

            string url = "https://raw.githubusercontent.com/grpzz/.mcex/master/mcex/version";
            string path = $"{mcexpath}lastversion";
            download1.DownloadFileAsync(new Uri(url), path);
        }

        private void cargado1(object sender, AsyncCompletedEventArgs e)
        {
            Stream stream = File.OpenRead($"{mcexpath}lastversion");
            StreamReader streamReader = new StreamReader(stream);
            string str = streamReader.ReadToEnd();

            lastversion = str;
            streamReader.Close();
            stream.Close();

            label3.Text = lastversion;

            if (label2.Text == label3.Text)
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
            string path = $"{mcexpath}mcexInstaller.exe";
            download2.DownloadFileAsync(new Uri(url), path);
        }

        private void cargado2(object sender, AsyncCompletedEventArgs e)
        {
            Process.Start($"{mcexpath}mcexInstaller.exe");
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Update();
            button1.Enabled = false;
        }
    }
}
