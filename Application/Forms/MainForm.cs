using GmailNotifierReplacement.Icons;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    // We need this form for the systray icon and the context menu, but it won't be visible
    public partial class MainForm : Form
    {
        // Logger
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainForm));

        // Settings defaults
        private const int m_notificationDelay_Min = 1; //seconds
        private const int m_notificationDelay_Max = 60;
        private const int m_notificationDelay_Default = 5;
        private const int m_CheckInterval_Min = 1; //minutes
        private const int m_CheckInterval_Max = 60;
        private const int m_CheckInterval_Default = 1;

        // These settings are configurable via the app.config file
        private int notificationDelay;
        private String gmailUrl;

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
            LoadConfig();

            // Initialize the ATOM client
            if (ConfigurationManager.AppSettings["AtomFeedUrl"].ToString() == string.Empty)
            {
              throw new ArgumentNullException("[AtomFeedUrl]", "Invalid config: [AtomFeedUrl] not set !");
            }
            this.atomMailChecker = new AtomMailChecker(ConfigurationManager.AppSettings["AtomFeedUrl"]);

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

        // Loads the configuration
        private void LoadConfig()
        {
            // Set the notification baloon delay
            int a_notificationDelay = Convert.ToInt32(ConfigurationManager.AppSettings["NotificationDelay"]);
            if (a_notificationDelay > m_notificationDelay_Max || a_notificationDelay < m_notificationDelay_Min)
            {
                Logger.Warn("Invalid config: [NotificationDelay] = " + a_notificationDelay.ToString() + " ! ...using default");
                a_notificationDelay = m_notificationDelay_Default;
            }
            this.notificationDelay = a_notificationDelay * 1000; // (convert seconds to milliseconds)
  
            // Set the timer interval from the config
            int a_CheckInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CheckInterval"]);
            if (a_CheckInterval > 60 || a_CheckInterval < 1)
            {
                Logger.Warn("Invalid config: [CheckInterval] = " + a_CheckInterval.ToString() + " ! ...using default");
                a_CheckInterval = m_CheckInterval_Default;
            }
            timer.Interval = a_CheckInterval * 60 * 1000; // (convert seconds to milliseconds)
  
            //Gmail url
            if (ConfigurationManager.AppSettings["GoogleMailUrl"].ToString().Length == 0)
            {
                Logger.Warn("Invalid config: [GoogleMailUrl] = " + ConfigurationManager.AppSettings["GoogleMailUrl"].ToString() + " ! ...using default");
                this.gmailUrl = "https://www.google.com/accounts/ServiceLogin?service=mail";
            }
            else
            {
                this.gmailUrl = ConfigurationManager.AppSettings["GoogleMailUrl"].ToString();
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
        private void CheckMail(bool showLast = false)
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
                    if (showLast)
                    {
                      notifyIcon.BalloonTipTitle = "Gmail";
                      notifyIcon.BalloonTipText = "Please set username/password in Options";
                      notifyIcon.ShowBalloonTip(notificationDelay);
                    }
                    return;
                }

                // Check for new mail
                atomMailChecker.Refresh(username, password);
                unreadCount = atomMailChecker.GetUnreadEmailCount();
                Logger.Debug("Unread mail count: " + unreadCount);

                // Update the icon
                notifyIcon.Icon = unreadCount > 0 ? SystrayIcons.SystrayIconBlue : SystrayIcons.SystrayIconFaded;
                notifyIcon.Text = unreadCount <= 0 ? "No unread mail" :
                                  unreadCount == 1 ? "1 unread email" :
                                  string.Format("{0} unread emails", unreadCount);

                // Show a notification
                if (unreadCount > 0)
                {
                    if (showLast)
                        lastMail = null;

                    var newEmails = atomMailChecker.GetNewEmails(lastMail);

                    if (showLast)
                    {
                        notifyIcon.BalloonTipTitle = "Last mail from:  " + newEmails.First().From;
                        notifyIcon.BalloonTipText = "[Subject:] " + newEmails.First().Subject + Environment.NewLine + "[Preview:] " + newEmails.First().Preview;
                        notifyIcon.ShowBalloonTip(notificationDelay);
                    }
                    else
                    {
                        if (newEmails.Count() == 1)
                        {
                            notifyIcon.BalloonTipTitle = "New mail from:  " + newEmails.Single().From;
                            notifyIcon.BalloonTipText = "[Subject:] " + newEmails.Single().Subject + Environment.NewLine + "[Preview:] " + newEmails.Single().Preview;
                            notifyIcon.ShowBalloonTip(notificationDelay);
                        }
                        if (newEmails.Count() > 1)
                        {
                            notifyIcon.BalloonTipTitle = "You've got mail";
                            notifyIcon.BalloonTipText = string.Format("You have {0} new emails", unreadCount);
                            notifyIcon.ShowBalloonTip(notificationDelay);
                        }
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

                var webException = x as WebException;
                if (webException != null)
                {
                    var response = webException.Response as HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized)
                        notifyIcon.Text = "Check credentials and make sure to use an app-specific password";
                }
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
                    else if (string.IsNullOrEmpty(UserSettings.Default.Username) || string.IsNullOrEmpty(UserSettings.Default.Password))
                    {
                        Logger.Warn("Missing credentials");
                        notifyIcon.Icon = SystrayIcons.SystrayIconError;
                        notifyIcon.Text = "Please set username/password in Options";
                    }

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
            Process.Start(gmailUrl);
        }

        private void viewInboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: view inbox");
            Process.Start(gmailUrl);
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

        private void lastEmailInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Context menu: show last email");
            CheckMail(true);
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Logger.Debug("Baloon tooltip: clicked");
            if (unreadCount > 0)
            {
                Process.Start(gmailUrl);
            }
        }

        #endregion
    }
}
