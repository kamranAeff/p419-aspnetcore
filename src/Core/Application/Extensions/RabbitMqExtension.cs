using RabbitMQ.Client;

namespace Application.Extensions
{
    public static partial class Extension
    {
        public static IConnection BuildRabbitMqConnection(this string connectionString)
        {
            var uri = new Uri(connectionString);
            var factory = new ConnectionFactory
            {
                HostName = uri.Host,
                UserName = uri.UserInfo.Split(':')[0],
                Password = uri.UserInfo.Split(':')[1],
                Port = (int)uri.Port,
                VirtualHost = uri.AbsolutePath.TrimStart('/'), // Remove the leading '/'
            };

            return factory.CreateConnection();
        }
    }
}
