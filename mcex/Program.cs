using mcex;
using System;
using System.IO;
using System.Windows.Forms;

namespace minecraftExplorer
{
    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
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
                        Application.Exit();
                        break;
                }
            }
            catch
            {
                options();
            }
        }

        private static void options()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new options());
        }

        private static void additem()
        {
            bool bm;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "ini", out bm);

            if (!bm)
            {
                mcexclass.GetdirectoryFiles();
                Application.Exit();
            }
            else
            {
                mcexclass.GetdirectoryFiles();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ini());
            }
            m.ReleaseMutex();
        }
    }
}
