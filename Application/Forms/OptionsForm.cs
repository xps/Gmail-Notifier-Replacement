using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GmailNotifierReplacement
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            usernameTextBox.Text = UserSettings.Default.Username;

            if (!string.IsNullOrEmpty(UserSettings.Default.Password))
                passwordTextBox.Text = "********";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            UserSettings.Default.Username = usernameTextBox.Text.Trim();

            if (passwordTextBox.Text != "********")
                UserSettings.Default.Password = PasswordEncryption.EncryptString(passwordTextBox.Text);

            UserSettings.Default.Save();

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void appPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://myaccount.google.com/apppasswords");
        }
    }
}
