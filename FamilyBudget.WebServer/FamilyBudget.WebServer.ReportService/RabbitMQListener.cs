
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using FamilyBudget.WebServer.ReportService.Data.Models;
using FamilyBudget.WebServer.ReportService.Services.Email;
using System.Net.Mail;
using System.Net;
using Aspose.Cells;
using FamilyBudget.WebServer.ReportService.Services.Excel;

namespace FamilyBudget.WebServer.ReportService
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IServiceScopeFactory serviceScopeFactory;
        public RabbitMQListener(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var reportMessage = JsonSerializer.Deserialize<ReportMessage>(content);
                string message;
                string receiver;
                Workbook workbook;
                if (reportMessage.ReportType == "user")
                {
                    message = $"{reportMessage.UserData.Email} budget report {DateTime.Now.Date}.";
                    receiver = reportMessage.EmailReport;
                    using (IServiceScope scope = serviceScopeFactory.CreateScope())
                    {
                        var userExcelService = scope.ServiceProvider.GetRequiredService<UserExcelService>();
                        workbook = userExcelService.GenerateReport(reportMessage.UserData);
                    }
                }
                else
                {
                    message = $"{reportMessage.FamilyData.Name} budget report {DateTime.Now.Date}.";
                    receiver = reportMessage.EmailReport;
                    using (IServiceScope scope = serviceScopeFactory.CreateScope())
                    {
                        var familyExcelService = scope.ServiceProvider.GetRequiredService<FamilyExcelService>();
                        workbook = familyExcelService.GenerateReport(reportMessage.FamilyData);
                    }
                }
                using (IServiceScope scope = serviceScopeFactory.CreateScope())
                {
                    var emailSender = scope.ServiceProvider.GetRequiredService<EmailSender>();
                    await emailSender.SendAsync(message, "vladkuchinskyi@gmail.com", workbook);
                }
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("MyQueue", false, consumer);


            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
