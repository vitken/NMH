using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;


namespace NMH_WebAPI.HostedService
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly string queueName = "inputs";
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "rabbituser",
                Password = "Nmh123!",
                VirtualHost = "/",
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, durable: true, exclusive: true);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, args) =>
            {
                var body = args.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);

            return Task.CompletedTask;
        }
    }
}
