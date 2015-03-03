namespace GmailNotifierReplacement
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewInboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMailNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tellMeAgainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastEmailInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "You have no unread mail.";
            this.notifyIcon.BalloonTipTitle = "Gmail";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "No unread mail";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewInboxToolStripMenuItem,
            this.checkMailNowToolStripMenuItem,
            this.tellMeAgainToolStripMenuItem,
            this.lastEmailInfoToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(162, 164);
            // 
            // viewInboxToolStripMenuItem
            // 
            this.viewInboxToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.viewInboxToolStripMenuItem.Name = "viewInboxToolStripMenuItem";
            this.viewInboxToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.viewInboxToolStripMenuItem.Text = "View &Inbox";
            this.viewInboxToolStripMenuItem.Click += new System.EventHandler(this.viewInboxToolStripMenuItem_Click);
            // 
            // checkMailNowToolStripMenuItem
            // 
            this.checkMailNowToolStripMenuItem.Name = "checkMailNowToolStripMenuItem";
            this.checkMailNowToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.checkMailNowToolStripMenuItem.Text = "&Check Mail Now";
            this.checkMailNowToolStripMenuItem.Click += new System.EventHandler(this.checkMailNowToolStripMenuItem_Click);
            // 
            // tellMeAgainToolStripMenuItem
            // 
            this.tellMeAgainToolStripMenuItem.Name = "tellMeAgainToolStripMenuItem";
            this.tellMeAgainToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.tellMeAgainToolStripMenuItem.Text = "Tell Me A&gain";
            this.tellMeAgainToolStripMenuItem.Click += new System.EventHandler(this.tellMeAgainToolStripMenuItem_Click);
            // 
            // lastEmailInfoToolStripMenuItem
            // 
            this.lastEmailInfoToolStripMenuItem.Name = "lastEmailInfoToolStripMenuItem";
            this.lastEmailInfoToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.lastEmailInfoToolStripMenuItem.Text = "&Last email info";
            this.lastEmailInfoToolStripMenuItem.Click += new System.EventHandler(this.lastEmailInfoToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 300000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 61);
            this.Name = "MainForm";
            this.Text = "Gmail Notifier Replacement";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewInboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkMailNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tellMeAgainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastEmailInfoToolStripMenuItem;
    }
}

