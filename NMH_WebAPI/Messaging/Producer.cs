using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace NMH_WebAPI.Messaging
{
    public class Producer : IProducer
    {
        private readonly string queueName = "inputs";
        public void SendMessage<T>(T message)
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

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", queueName, body: body);
        }
    }
}
