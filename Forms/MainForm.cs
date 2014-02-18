using GmailNotifierReplacement.Icons;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    // We need this form for the systray icon and the context menu, but it won't be visible
    public partial class MainForm : Form
    {
        // Logger
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainForm));

        // These settings are configurable via the app.config file
        private int notificationDelay;

        // The current state of the inbox
        private int unreadCount = 0;

        // The ATOM client
        private AtomMailChecker atomMailChecker;

        // The about form (singleton)
        private AboutForm aboutForm;

        // Constructor
        public MainForm()
        {
            // Initialize WinForms controls
            InitializeComponent();

            // Load the configuration
            this.notificationDelay = Convert.ToInt32(ConfigurationManager.AppSettings["NotificationDelay"]);

            // Initialize the ATOM client
            this.atomMailChecker = new AtomMailChecker(
                ConfigurationManager.AppSettings["AtomFeedUrl"],
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]
            );

            // Set the timer interval from the config (convert minutes to milliseconds)
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["CheckInterval"]) * 60 * 1000;

            // Don't wait for the first timer event
            CheckMail();
        }

        // This makes sure that this window is always hidden
        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                value = false;
            }

            base.SetVisibleCore(value);
        }

        // Connects to the IMAP server to check for new mail
        private void CheckMail()
        {
            Logger.Debug("Checking mail...");

            try
            {
                atomMailChecker.Refresh();

                unreadCount = atomMailChecker.GetUnreadMailCount();
                
                Logger.Debug("Unread mail count: " + unreadCount);

                notifyIcon.Icon = unreadCount > 0 ? SystrayIcons.SystrayIconBlue : SystrayIcons.SystrayIconFaded;
                notifyIcon.Text = unreadCount > 0 ? string.Format("{0} unread emails", unreadCount) : "No unread mail";
            }
            catch (Exception x)
            {
                Logger.Warn("Error while checking mail", x);

                unreadCount = 0;
                notifyIcon.Icon = SystrayIcons.SystrayIconError;
                notifyIcon.Text = "Couldn't check mail";
            }
        }

        // This is what checks mail regularly
        private void timer_Tick(object sender, EventArgs e)
        {
            CheckMail();
        }

        #region Context Menu Handlers

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Logger.Debug("System tray icon: double click");
            Process.Start("https://mail.google.com");
        }

        private void viewInboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: view inbox");
            Process.Start("https://mail.google.com");
        }

        private void checkMailNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: check mail now");
            CheckMail();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: about");

            lock (this)
            {
                if (aboutForm == null)
                    aboutForm = new AboutForm();
            }

            aboutForm.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: exit");
            Application.Exit();
        }

        #endregion
    }
}
