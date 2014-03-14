using GmailNotifierReplacement.Icons;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
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
        private int unreadCount;
        private DateTime? lastMail;

        // The ATOM client
        private AtomMailChecker atomMailChecker;

        // Forms
        private AboutForm aboutForm;
        private OptionsForm optionsForm;

        // Constructor
        public MainForm()
        {
            // Initialize WinForms controls
            InitializeComponent();

            // Load the configuration
            this.notificationDelay = Convert.ToInt32(ConfigurationManager.AppSettings["NotificationDelay"]);

            // Initialize the ATOM client
            this.atomMailChecker = new AtomMailChecker(ConfigurationManager.AppSettings["AtomFeedUrl"]);

            // Set the timer interval from the config (convert minutes to milliseconds)
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["CheckInterval"]) * 60 * 1000;

            // Have we set the username/password already?
            if (string.IsNullOrEmpty(UserSettings.Default.Username))
            {
                // Ask for credentials
                ShowOptions();
            }
            else
            {
                // Don't wait for the first timer event
                CheckMail();
            }
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

        // Downloads the latest Atom feed to check for new mail
        private void CheckMail()
        {
            Logger.Debug("Checking mail...");

            try
            {
                // Get credentials
                var username = UserSettings.Default.Username;
                var password = PasswordEncryption.DecryptString(UserSettings.Default.Password);
                if (string.IsNullOrEmpty(username))
                {
                    Logger.Warn("Missing credentials");
                    notifyIcon.Icon = SystrayIcons.SystrayIconError;
                    notifyIcon.Text = "Please set username/password in Options";
                    return;
                }

                // Check for new mail
                atomMailChecker.Refresh(username, password);
                unreadCount = atomMailChecker.GetUnreadEmailCount();
                Logger.Debug("Unread mail count: " + unreadCount);

                // Update the icon
                notifyIcon.Icon = unreadCount > 0 ? SystrayIcons.SystrayIconBlue : SystrayIcons.SystrayIconFaded;
                notifyIcon.Text = unreadCount > 0 ? string.Format("{0} unread emails", unreadCount) : "No unread mail";

                // Show a notification
                if (unreadCount > 0)
                {
                    var newEmails = atomMailChecker.GetNewEmails(lastMail);

                    if (newEmails.Count() == 1)
                    {
                        notifyIcon.BalloonTipTitle = "New mail from: " + newEmails.Single().From;
                        notifyIcon.BalloonTipText = newEmails.Single().Subject + Environment.NewLine + newEmails.Single().Preview;
                        notifyIcon.ShowBalloonTip(notificationDelay);
                    }
                    if (newEmails.Count() > 1)
                    {
                        notifyIcon.BalloonTipTitle = "You've got mail";
                        notifyIcon.BalloonTipText = string.Format("You have {0} new emails", unreadCount);
                        notifyIcon.ShowBalloonTip(notificationDelay);
                    }                    
                }
                else
                {
                    notifyIcon.BalloonTipTitle = "No unread mail";
                    notifyIcon.BalloonTipText = "You have no unread mail.";
                }

                // Remember the last email that we showed
                lastMail = atomMailChecker.GetLastEmailDate();
            }
            catch (Exception x)
            {
                Logger.Warn("Error while checking mail", x);

                notifyIcon.Icon = SystrayIcons.SystrayIconError;
                notifyIcon.Text = "Couldn't check mail";
            }
        }

        // This is what checks mail regularly
        private void timer_Tick(object sender, EventArgs e)
        {
            CheckMail();
        }

        // Shows the Options form
        private void ShowOptions()
        {
            if (optionsForm == null)
            {
                optionsForm = new OptionsForm();
                optionsForm.FormClosed += (form, _) =>
                {
                    if (optionsForm.DialogResult == DialogResult.OK)
                        CheckMail();

                    optionsForm = null;
                };
            }

            optionsForm.Show();
        }

        // Shows the About form
        private void ShowAbout()
        {
            if (aboutForm == null)
            {
                aboutForm = new AboutForm();
                aboutForm.FormClosed += (form, _) =>
                {
                    aboutForm = null;
                };
            }

            aboutForm.Show();
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

        private void tellMeAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(notificationDelay);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: options");
            ShowOptions();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: about");
            ShowAbout();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: exit");
            Application.Exit();
        }

        #endregion
    }
}
