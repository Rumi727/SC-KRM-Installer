using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCKRM.Installer
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool loop;
            do
            {
                loop = false;

                try
                {
                    Application.Run(new MainForm());
                }
                catch (Exception e)
                {
                    Exception(e);
                    loop = true;
                }

                AsyncTaskManager.AllAsyncTaskCancel();
            }
            while (loop);
        }

        public static void Exception(Exception e) => MessageBox.Show(e.Message + "\n\n" + e.StackTrace.Substring(5), e.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
