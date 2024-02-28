namespace FamilyBudget.WebServer.Mvc.Services.RabbitMq
{
    public interface IRabbitMQService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
