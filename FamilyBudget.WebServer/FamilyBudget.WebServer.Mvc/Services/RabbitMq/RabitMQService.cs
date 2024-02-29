using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FamilyBudget.WebServer.Mvc.Services.RabbitMq
{
    public class RabitMQService : IRabbitMQService
    {
        private string HostName;
        private string Queue;

        public RabitMQService(RabbitMqConfiguration rabbitMqConfig)
        {
            this.HostName = rabbitMqConfig.HostName;
            this.Queue = rabbitMqConfig.Queue;
        }

        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            // Не забудьте вынести значения "localhost" и "MyQueue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyQueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: "MyQueue",
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
