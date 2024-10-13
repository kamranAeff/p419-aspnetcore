using Application.Services;
using Domain.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Services.Registration;
using System.Net;
using System.Net.Mail;
using System.Text;
using Application.Extensions;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace Services
{
    [SingletonLifeTime]
    class EmailService : SmtpClient, IEmailService
    {
        private readonly EmailConfiguration options;

        public EmailService(IOptions<EmailConfiguration> options)
        {
            this.options = options.Value;

            Host = this.options.Host;
            Port = this.options.Port;
            EnableSsl = this.options.EnableSsl;
            Credentials = new NetworkCredential(this.options.UserName, this.options.Password);
        }

        public async Task SendEmail(SendEmailRequest request)
        {
            using (var message = new MailMessage())
            {
                request.To = "akamran@code.edu.az";
                message.Subject = request.Subject;
                message.Body = request.Body;
                message.IsBodyHtml = true;
                message.From = new MailAddress(options.UserName, options.DisplayName);

                message.To.Add(request.To);

                //src=('|")cid:[^'"]*('|")

                var matches = Regex.Matches(message.Body, @"src=('|"")cid:(?<contentId>[^'""]*)('|"")");

                if (matches.Any())
                {
                    var avHtml = AlternateView.CreateAlternateViewFromString(message.Body, Encoding.Unicode, MediaTypeNames.Text.Html);
                    foreach (Match match in matches)
                    {
                        var contentId = match.Groups["contentId"].Value;
                        var inlineImage = new LinkedResource(Path.Combine("wwwroot", "email-templates", contentId), MediaTypeNames.Image.Jpeg);
                        inlineImage.ContentId = contentId;
                        avHtml.LinkedResources.Add(inlineImage);
                    }
                    message.AlternateViews.Add(avHtml);
                }
                

                
                

                await SendMailAsync(message);
            }
        }

        public Task SendEmailQueue(string to, string subject, string body)
        {
            var connection = Environment.GetEnvironmentVariable("RABBIT_URL")
                .BuildRabbitMqConnection();


            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "emails",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var request = new SendEmailRequest
            {
                To = to,
                Subject = subject,
                Body = body,
            };

            var requestJson = JsonConvert.SerializeObject(request);

            channel.BasicPublish(exchange: "",
                                 routingKey: "emails",
                                 basicProperties: null,
                                 body: Encoding.Unicode.GetBytes(requestJson));

            return Task.CompletedTask;
        }
    }
}
