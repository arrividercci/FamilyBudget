using Aspose.Cells;
using System.Net.Mail;
using System.Net;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.Formatters;
using MimeKit.Text;

namespace FamilyBudget.WebServer.ReportService.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync<Workbook>(string receiver, string messsage, Workbook attachment)
        {
            
        }
    }
}
