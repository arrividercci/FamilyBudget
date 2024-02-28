using FamilyBudget.WebServer.Data.Models;

namespace FamilyBudget.WebServer.Mvc.Services.RabbitMq
{
    public class ReportMessage
    {
        public string ReportType { get; set; }
        public UserDto UserData { get; set; }
        public FamilyDto FamilyData { get; set; }
        public string EmailReport { get; set; }
    }
}
