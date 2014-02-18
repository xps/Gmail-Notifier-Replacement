using System;
using System.IO;
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

        public int GetUnreadMailCount()
        {
            if (_feed == null)
                return 0;
            else
                return Convert.ToInt32(_feed.Root.Element(XName.Get("fullcount", "http://purl.org/atom/ns#")).Value);
        }
    }
}
