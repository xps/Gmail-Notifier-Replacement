using log4net;
using System;
using System.Threading;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    static class Program
    {
        // Logger
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Info("Program starting...");

            Application.ThreadException += HandleError;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
              Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
              Logger.Error("Error while running program!", ex);
              MessageBox.Show("Error while running program!\nSee log for details.\nProgram will close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Logger.Info("Program closed.");
        }

        private static void HandleError(object sender, ThreadExceptionEventArgs t)
        {
            Logger.Fatal("Unhandled exception", t.Exception);
        }
    }
}
