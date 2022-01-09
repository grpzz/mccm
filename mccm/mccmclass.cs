using System;
using System.IO;
using System.Windows.Forms;

namespace mccm
{
    class mccmclass
    {
        public static void itemsFile()
        {
            string itempath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @".mccm\items");

            if (File.Exists(itempath))
            {
                File.Delete(itempath);
                File.Create(itempath);
            }
            else
            {
                File.Create(itempath);
            }
        }

        public static void MoveFilesToMinecraft(string path)
        {
            string line;
            string itempath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @".mccm\items");

            StreamReader file = new System.IO.StreamReader(itempath);
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    FileInfo fi = new FileInfo(line);
                    System.IO.File.Move(line, path + @"\" + fi.Name);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            file.Close();
        }

        public static void GetdirectoryFiles()
        {
            string itempath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @".mccm\items");

            StreamWriter WriteReportFile = File.AppendText(itempath);
            WriteReportFile.WriteLine(Environment.GetCommandLineArgs()[2]);
            WriteReportFile.Close();
        }
    }
}
