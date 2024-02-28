namespace FamilyBudget.WebServer.ReportService.Services.Email
{
    public interface IEmailService
    {
        public Task SendAsync<T>(string receiver, string message, T attachment);
    }
}
