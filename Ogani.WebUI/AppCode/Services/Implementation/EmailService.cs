using Microsoft.Extensions.Options;
using Ogani.WebUI.Models.Configurations;
using System.Net;
using System.Net.Mail;

namespace Ogani.WebUI.AppCode.Services.Implementation
{
    public class EmailService : SmtpClient, IEmailService
    {
        private readonly EmailConfiguration options;

        public EmailService(IOptions<EmailConfiguration> options)
        {
            this.options = options.Value;

            this.Host = this.options.Host;
            this.Port = this.options.Port;
            this.EnableSsl = this.options.EnableSsl;
            this.Credentials = new NetworkCredential(this.options.UserName, this.options.Password);
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress(options.UserName,options.DisplayName);

                message.To.Add(to);

                await this.SendMailAsync(message);
            }
        }
    }
}
