using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            versionLabel.Text = string.Format("Version {0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/xps/gmail-notifier-replacement");
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
