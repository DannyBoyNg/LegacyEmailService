using System.Net;

namespace Ng.Services
{
    public class EmailSettings
    {
        /// <summary>
        /// The Hostname of the mailserver you wish to use.
        /// </summary>
        public string? Host { get; set; }
        /// <summary>
        /// The port number of the mailserver you wish to use. If not set
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// Credentials to authenticate to the mailserver
        /// </summary>
        public ICredentialsByHost? Credentials { get; set; }
        public bool? IsBodyHtml { get; set; }
        public bool? EnableSsl { get; set; }
    }
}
