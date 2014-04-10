using System;

namespace GmailNotifierReplacement
{
    public class EmailPreview
    {
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Preview { get; set; }
    }
}
