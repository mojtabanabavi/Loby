using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Loby.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Loby.Tools
{
    /// <summary>
    /// Allows applications to send email by using the 
    /// Simple Mail Transfer Protocol (SMTP).
    /// </summary>
    public class Mailer
    {
        #region Members

        private SmtpClient _client;
        private MailAddress _sender;
        public MailerSettings _settings;
        public readonly string Versian = "2.0";

        #endregion;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Mailer"/> 
        /// based on <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings">
        /// An instance of <see cref="MailerSettings"/> that contain 
        /// setting of how to send email.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// settings is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// settings.Host is null or empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// settings.Port is less than or equal to zero.
        /// </exception>
        public Mailer(MailerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "is null.");
            }

            if (!settings.Host.HasValue())
            {
                throw new ArgumentException(nameof(settings.Host), "is null or empty.");
            }

            if (settings.Port <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(settings.Port), "is less than or equal to zero.");
            }

            _settings = settings;

            ConfigureMailer();
        }

        #endregion;

        #region Configures

        /// <summary>
        /// Applying settings in current instance of mailer.
        /// </summary>
        /// <param name="settings">
        /// An instance of <see cref="MailerSettings"/> that contain 
        /// setting of how to send email.
        /// </param>
        private void ConfigureMailer()
        {
            var senderEmail =
                _settings.SenderEmailAddress ?? _settings.Username;

            _sender =
                new MailAddress(senderEmail, _settings.SenderDisplayName ?? senderEmail, Encoding.UTF8);

            _client = new SmtpClient
            {
                Host = _settings.Host,
                Port = _settings.Port,
                EnableSsl = _settings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _settings.UseDefaultCredentials,
            };

            if (!_settings.UseDefaultCredentials)
            {
                _client.Credentials =
                    new NetworkCredential(_settings.Username, _settings.Password);
            }
        }

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
            return ConfigureMailMessage(new string[] { recipient }, subject, body);
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

            var message = new MailMessage()
            {
                Body = body,
                From = _sender,
                Sender = _sender,
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
            };

            message.To.Add(_recipients);

            message.Headers.Add("Agent", "Loby.Mailer");
            message.Headers.Add("Agent.Version", Versian);

            return message;
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
