using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace GmailNotifierReplacement
{
    public class AtomMailChecker
    {
        private string _atomFeedUrl;
        private string _username;
        private string _password;

        private XDocument _feed;

        public AtomMailChecker(string atomFeedUrl, string username, string password)
        {
            this._atomFeedUrl = atomFeedUrl;
            this._username = username;
            this._password = password;
        }

        public void Refresh()
        {
            var request = WebRequest.Create(_atomFeedUrl);

            request.Credentials = new NetworkCredential(_username, _password);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                _feed = XDocument.Load(reader);
            }
        }

        public int GetUnreadEmailCount()
        {
            if (_feed == null)
                return 0;
            
            return Convert.ToInt32(_feed.Root.Element("{http://purl.org/atom/ns#}fullcount").Value);
        }

        public IEnumerable<EmailPreview> GetNewEmails(DateTime? since)
        {
            if (_feed == null)
                yield break;

            var ns = _feed.Root.GetDefaultNamespace();

            foreach (var entry in _feed.Root.Elements(ns + "entry"))
            {
                var date = Convert.ToDateTime(entry.Element(ns + "issued").Value);
                if (since == null || date > since)
                {
                    var author = entry.Element(ns + "author");

                    var from = author.Element(ns + "email").Value;
                    var authorName = author.Element(ns + "name");
                    if (authorName != null && !string.IsNullOrEmpty(authorName.Value))
                        from = authorName.Value;

                    var preview = entry.Element(ns + "summary").Value;
                    if (preview.Length > 80)
                        preview = preview.Substring(0, 80) + "...";

                    yield return new EmailPreview
                    {
                        Date = date,
                        From = author.Element(ns + "name").Value,
                        Subject = entry.Element(ns + "title").Value,
                        Preview = preview
                    };
                }
            }
        }

        public DateTime? GetLastEmailDate()
        {
            if (_feed == null)
                return null;

            var ns = _feed.Root.GetDefaultNamespace();

            return _feed.Root
                        .Elements(ns + "entry")
                        .Select(e => (DateTime?)Convert.ToDateTime(e.Element(ns + "issued").Value))
                        .Max();
        }
    }
}
