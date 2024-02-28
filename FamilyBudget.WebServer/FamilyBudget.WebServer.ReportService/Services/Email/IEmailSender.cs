namespace FamilyBudget.WebServer.ReportService.Services.Email
{
    public interface IEmailSender
    {
        void SendEmail(string message);
    }
}
