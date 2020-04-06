using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ng.Services
{
    public class LegacyEmailService : ILegacyEmailService
    {
        public EmailSettings Settings { get; set; }

        public LegacyEmailService() => Settings = new EmailSettings();

        public LegacyEmailService(EmailSettings settings) => Settings = settings ?? new EmailSettings();

        public LegacyEmailService(IOptions<EmailSettings> settings) => Settings = settings?.Value ?? new EmailSettings();

        public async Task SendEmail(MailAddress from, IEnumerable<string> to, string subject, string body, IEnumerable<Attachment>? files = null, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null, MailPriority? priority = null)
        {
            if (Settings.Host == null) throw new MailServerNotSetException();
            using var smtpClient = new SmtpClient();
            using var message = new MailMessage();

            smtpClient.Host = Settings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (Settings.Port != null) smtpClient.Port = Settings.Port.Value;
            if (Settings.EnableSsl != null) smtpClient.EnableSsl = Settings.EnableSsl.Value;
            if (Settings.Credentials != null)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = Settings.Credentials;
            }

            message.From = from;
            message.To.Add(string.Join(",", to));
            message.Subject = subject;
            message.Body = body;
            if (bcc != null) message.Bcc.Add(string.Join(",", bcc));
            if (cc != null) message.CC.Add(string.Join(",", cc));
            if (priority != null) message.Priority = priority.Value;
            if (Settings.IsBodyHtml != null) message.IsBodyHtml = Settings.IsBodyHtml.Value;

            if (files != null)
            {
                foreach (var file in files)
                {
                    var stream = new MemoryStream(file.FileData as byte[]);
                    var attachment = new System.Net.Mail.Attachment(stream, file.FileName);
                    message.Attachments.Add(attachment);
                }
            }

            var task = smtpClient.SendMailAsync(message);
            await task.ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Exceptions not of interest. Just need to know if email address is valid or not.")]
        public bool IsEmailValid(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
