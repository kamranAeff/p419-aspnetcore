namespace Application.Services
{
    public interface IEmailService
    {
        Task SendEmail(SendEmailRequest request);
        Task SendEmailQueue(string to, string subject, string body);
    }




    public class SendEmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
