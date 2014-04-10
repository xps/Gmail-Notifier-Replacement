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
            Application.Run(new MainForm());

            Logger.Info("Program closed.");
        }

        private static void HandleError(object sender, ThreadExceptionEventArgs t)
        {
            Logger.Fatal("Unhandled exception", t.Exception);
        }
    }
}
