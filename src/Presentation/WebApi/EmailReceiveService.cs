using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Application.Extensions;
using Newtonsoft.Json;
using Application.Services;
using System.Threading.Channels;

namespace WebApi
{
    class EmailReceiveService : IHostedService
    {
        private readonly IEmailService emailService;
        IConnection connection = default;
        IModel channel = default;

        public EmailReceiveService(IEmailService emailService)
        {
            connection = Environment.GetEnvironmentVariable("RABBIT_URL")
                .BuildRabbitMqConnection();

            channel = connection.CreateModel();
            this.emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var queue = channel.QueueDeclare("emails", true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, e) =>
            {
                string json = Encoding.Unicode.GetString(e.Body.ToArray());

                var request = JsonConvert.DeserializeObject<SendEmailRequest>(json);

                await emailService.SendEmail(request);

                Console.WriteLine(json);

                channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Dispose();
            connection.Dispose();

            return Task.CompletedTask;
        }
    }
}
