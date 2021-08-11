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
            Version();

            download1 = new WebClient();
            download2 = new WebClient();
            download1.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed1);
            download2.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
        }

        private void Version()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            actuallyversion = fvi.FileVersion;
            label2.Text = actuallyversion;
        }

        private void SearchVersion()
        {
            string url = "https://raw.githubusercontent.com/grpzz/.mcex/master/mcex/version";
            string path = $"{mcexpath}lastversion";
            download1.DownloadFileAsync(new Uri(url), path);
        }

        private void Completed1(object sender, AsyncCompletedEventArgs e)
        {
            Stream stream = File.OpenRead($"{mcexpath}lastversion");
            StreamReader streamReader = new StreamReader(stream);
            string str = streamReader.ReadToEnd();

            lastversion = str;
            streamReader.Close();
            stream.Close();
            Directory.Delete($"{mcexpath}lastversion");

            label3.Text = lastversion;
        }

        private void download()
        {
            string url = "https://raw.githubusercontent.com/grpzz/.mcex/master/mcexInstaller/bin/mcexInstaller.exe";
            string path = $"{mcexpath}mcexInstaller.exe";
            download2.DownloadFileAsync(new Uri(url), path);
        }

        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            if (Directory.Exists($"{mcexpath}mcexInstaller.exe"))
            {
                try
                {
                    Process.Start($"{mcexpath}mcexInstaller.exe");
                    Application.Exit();
                }
                catch (Win32Exception)
                {
                    MessageBox.Show("Application start error");
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("File not Found");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            download();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchVersion();
            button2.Enabled = false;
        }
    }
}
