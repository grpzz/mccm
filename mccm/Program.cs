using mccm;
using System;
using System.Windows.Forms;

namespace minecraftcontextmenu
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                string commandline = Environment.GetCommandLineArgs()[1];
                switch (commandline)
                {
                    case "additem":
                        additem();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                options();
                Console.WriteLine(e);
            }
        }

        private static void options()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new options());
        }

        /*private static void install()
        {
            string programpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".mcex\");
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string sversion = fvi.FileVersion;

            File.Create($"{programpath}version");
            StreamWriter WriteReportFile = File.AppendText($"{programpath}version");
            WriteReportFile.WriteLine(sversion);
            WriteReportFile.Close();
        }*/

        private static void additem()
        {
            bool bm;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "ini", out bm);

            if (!bm)
            {
                mccmclass.GetdirectoryFiles();
                Application.Exit();
            }
            else
            {
                mccmclass.GetdirectoryFiles();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ini());
            }
            m.ReleaseMutex();
        }
    }
}
