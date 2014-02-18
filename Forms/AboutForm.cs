using System.Diagnostics;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/xps/gmail-notifier-replacement");
        }
    }
}
