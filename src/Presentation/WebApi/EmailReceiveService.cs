using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Application.Extensions;

namespace WebApi
{
    class EmailReceiveService : IHostedService
    {
        IConnection connection = default;
        IModel channel = default;

        public EmailReceiveService()
        {
            connection = Environment.GetEnvironmentVariable("RABBIT_URL")
                .BuildRabbitMqConnection();

            channel = connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                string json = Encoding.Unicode.GetString(e.Body.ToArray());

                Console.WriteLine(json);

                channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };

            channel.BasicConsume("emails", false, consumer);

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
