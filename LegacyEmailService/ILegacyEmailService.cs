using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DannyBoyNg.Services
{
    public interface ILegacyEmailService
    {
        public EmailSettings Settings { get; set; }

        bool IsEmailValid(string email);
        Task SendEmail(MailAddress from, IEnumerable<string> mailto, string subject, string body, IEnumerable<Attachment>? files = null, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null, MailPriority? priority = null);
    }
}
