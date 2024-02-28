namespace FamilyBudget.WebServer.ReportService.Data.Models
{
    public class ReportMessage
    {
        public string ReportType { get; set; }
        public UserDto UserData { get; set; }
        public FamilyDto FamilyData { get; set; }
        public string EmailReport { get; set; }
    }
}
