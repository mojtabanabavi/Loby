using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Loby.Tools.Models
{
    public class MailerSettings
    {
        /// <summary>
        /// The port used for SMTP transactions. The default value is 25.
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// The name or IP address of the host used for SMTP transactions.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// A number that specifies the amount of time in milliseconds after 
        /// which a synchronous Send call times out. The default value is 
        /// 100,000 (100 seconds)
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// Specify whether the <see cref="Mailer"/> uses Secure Sockets Layer (SSL)
        /// to encrypt the connection. The default is false.
        /// </summary>
        public bool EnableSsl { get; set; } = false;

        /// <summary>
        /// The username associated with the SMTP server credentials.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password for the username associated with SMTP server the credentials.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Specify whether the <see cref="CredentialCache.DefaultCredentials"/> are sent with requests.
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// An string that contains the address of the sender of the e-mail message.
        /// </summary>
        public string SenderEmailAddress { get; set; }

        /// <summary>
        /// An string that contains a display name as sender email address.
        /// </summary>
        public string SenderDisplayName { get; set; }

    }
}