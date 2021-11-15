using System;
using System.Net;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Loby
{
    /// <summary>
    /// Allows applications to send email by using the 
    /// Simple Mail Transfer Protocol (SMTP).
    /// </summary>
    public class Mailer
    {
        #region Members

        private string _from;
        private SmtpClient _client;

        public enum ClientTypes
        {
            Gmail = 1,
            Yahoo = 2,
        }

        #endregion;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Mailer"/> class that
        /// sends email by using the specified SMTP server, port and credential.
        /// </summary>
        /// <param name="host">
        /// A string that contains the name or IP address of the host used for SMTP
        /// transactions.
        /// </param>
        /// <param name="port">
        /// A number greater than zero that contains the port to be used on host.
        /// </param>
        /// <param name="username">
        /// The user name associated with the SMTP server credentials.
        /// </param>
        /// <param name="password">
        /// The password for the user name associated with SMTP server the credentials.
        /// </param>
        public Mailer(string host, int port, string username, string password)
        {
            _from = username;

            _client = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mailer"/> class by using predefined configurations
        /// according to <paramref name="clientType"/>.
        /// </summary>
        /// <param name="clientType">
        /// The type of SMTP client that settings are applied according to it.
        /// </param>
        /// <param name="username">
        /// The user name associated with the SMTP server credentials.
        /// </param>
        /// <param name="password">
        /// The password for the user name associated with SMTP server the credentials.
        /// </param>
        public Mailer(ClientTypes clientType, string username, string password)
        {
            _from = username;

            switch (clientType)
            {
                case ClientTypes.Gmail: _client = GetGmailClient(username, password); break;
                case ClientTypes.Yahoo: _client = GetYahooClient(username, password); break;
            }
        }

        #endregion;

        #region Sends

        /// <summary>
        /// Sends the specified email message to an SMTP server for delivery.
        /// </summary>
        /// <param name="recipient">
        /// A string that contains the address for sending message to it.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="SmtpException">
        /// The connection to the SMTP server failed. -or- Authentication failed. -or- The
        /// operation timed out -or- ssl is set to true, but the SMTP mail server did not 
        /// advertise STARTTLS in the response to the EHLO command.
        /// </exception>
        /// <exception cref="SmtpFailedRecipientException">
        /// The message could not be delivered to recipient.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// recipient is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipient contains an invalid email address.
        /// </exception>
        public void Send(string recipient, string subject, string body)
        {
            var mail = ConfigureMailMessage(recipient, subject, body);

            _client.Send(mail);
        }

        /// <summary>
        /// Sends the specified email message to an SMTP server for delivery as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="recipient">
        /// A string that contains the address for sending message to it.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="SmtpException">
        /// The connection to the SMTP server failed. -or- Authentication failed. -or- The
        /// operation timed out -or- ssl is set to true, but the SMTP mail server did not 
        /// advertise STARTTLS in the response to the EHLO command.
        /// </exception>
        /// <exception cref="SmtpFailedRecipientException">
        /// The message could not be delivered to recipient.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// recipient is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipient contains an invalid email address.
        /// </exception>
        public Task SendAsync(string recipient, string subject, string body)
        {
            var mail = ConfigureMailMessage(recipient, subject, body);

            return _client.SendMailAsync(mail);
        }

        /// <summary>
        /// Sends the specified email message to an SMTP server for delivery.
        /// </summary>
        /// <param name="recipients">
        /// A collection of recipient addresses for sending message to them.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="SmtpException">
        /// The connection to the SMTP server failed. -or- Authentication failed. -or- The
        /// operation timed out -or- ssl is set to true, but the SMTP mail server did not 
        /// advertise STARTTLS in the response to the EHLO command.
        /// </exception>
        /// <exception cref="SmtpFailedRecipientException">
        /// The message could not be delivered to two or more of the recipients.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipients is null or empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipients contains one or more invalid email address.
        /// </exception>
        public void Send(IEnumerable<string> recipients, string subject, string body)
        {
            var mail = ConfigureMailMessage(recipients, subject, body);

            _client.Send(mail);
        }

        /// <summary>
        /// Sends the specified email message to an SMTP server for delivery as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="recipients">
        /// A collection of recipient addresses for sending message to them.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="SmtpException">
        /// The connection to the SMTP server failed. -or- Authentication failed. -or- The
        /// operation timed out -or- ssl is set to true, but the SMTP mail server did not 
        /// advertise STARTTLS in the response to the EHLO command.
        /// </exception>
        /// <exception cref="SmtpFailedRecipientException">
        /// The message could not be delivered to two or more of the recipients.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipients is null or empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipients contains one or more invalid email address.
        /// </exception>
        public Task SendAsync(IEnumerable<string> recipients, string subject, string body)
        {
            var mail = ConfigureMailMessage(recipients, subject, body);

            return _client.SendMailAsync(mail);
        }

        #endregion;

        #region Clients

        /// <summary>
        /// Creates a new instance of <see cref="SmtpClient"/> that fits 
        /// the gmail smtp server settings.
        /// </summary>
        /// <param name="username">
        /// Your email address in the Gmail service.
        /// </param>
        /// <param name="password">
        /// The password associated to your gmail account.
        /// </param>
        /// <returns>
        /// Returns An instance of <see cref="SmtpClient"/> that fits 
        /// the gmail smtp server settings.
        /// </returns>
        public SmtpClient GetGmailClient(string username, string password)
        {
            var client = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            return client;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SmtpClient"/> that fits 
        /// the yahoo smtp server settings.
        /// </summary>
        /// <param name="username">
        /// Your email address in the Yahoo service.
        /// </param>
        /// <param name="password">
        /// The password associated to your Yahoo account.
        /// </param>
        /// <returns>
        /// Returns An instance of <see cref="SmtpClient"/> that fits 
        /// the Yahoo smtp server settings.
        /// </returns>
        public SmtpClient GetYahooClient(string username, string password)
        {
            var client = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.mail.yahoo.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            return client;
        }

        #endregion;

        #region Configures

        /// <summary>
        /// Creates a new instance of <see cref="MailMessage"/> based on
        /// inputs and default settings.
        /// </summary>
        /// <param name="recipient">
        /// A string that contains the address for sending message to it.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// recipient is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipient contains an invalid email address.
        /// </exception>
        private MailMessage ConfigureMailMessage(string recipient, string subject, string body)
        {
            if (recipient.IsNull())
            {
                throw new ArgumentNullException(nameof(recipient), "is null.");
            }

            if (!Validator.IsValidEmail(recipient))
            {
                throw new ArgumentException(nameof(recipient), "contains an invalid email address.");
            }

            var mail = new MailMessage(_from, recipient)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
            };

            return mail;
        }

        /// <summary>
        /// Creates a new instance of <see cref="MailMessage"/> based on
        /// inputs and default settings.
        /// </summary>
        /// <param name="recipients">
        /// A collection of recipient addresses for sending message to them.
        /// </param>
        /// <param name="subject">
        /// A string that contains the subject line for the message.
        /// </param>
        /// <param name="body">
        /// A string that contains the message body.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// recipients is null or empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// recipients contains one or more invalid email address.
        /// </exception>
        private MailMessage ConfigureMailMessage(IEnumerable<string> recipients, string subject, string body)
        {
            if (recipients.IsNull())
            {
                throw new ArgumentException(nameof(recipients), "is null or empty.");
            }

            if (!IsValidEmails(recipients))
            {
                throw new ArgumentException(nameof(recipients), "contains one or more invalid email address.");
            }

            var _recipients = recipients.Join(',');

            var mail = new MailMessage(_from, _recipients)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
            };

            return mail;
        }

        #endregion;

        #region Validations

        /// <summary>
        /// Indicates whether the specified list is contaiing valid email addresses or not.
        /// </summary>
        /// <param name="emails">
        /// The emails collection to test.
        /// </param>
        /// <returns>
        /// true if the value parameter is a valid email address; otherwise, false.
        /// </returns>
        private bool IsValidEmails(IEnumerable<string> emails)
        {
            var _emails = emails ?? Enumerable.Empty<string>();

            foreach (var email in _emails)
            {
                var validationResult = Validator.IsValidEmail(email);

                if (!validationResult)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion;
    }
}
