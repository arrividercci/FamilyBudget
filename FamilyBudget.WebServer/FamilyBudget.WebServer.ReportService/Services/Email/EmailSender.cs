using Aspose.Cells;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace FamilyBudget.WebServer.ReportService.Services.Email
{
    public class EmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration _emailConfig)
        {
            this._emailConfig = _emailConfig;
        }

        public async Task SendAsync(string message, string receiver, Workbook workbook)
        {
            var smtpClient = new SmtpClient(_emailConfig.SmtpServer)
            {
                Port = _emailConfig.Port,
                Credentials = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.From),
                Subject = "Family Budget report",
                Body = message,
                IsBodyHtml = true
            };

            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Save(stream, SaveFormat.Xlsx);

                stream.Position = 0;

                var attachment = new Attachment(stream, "BudgetReport", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                mailMessage.Attachments.Add(attachment);

                mailMessage.To.Add(receiver);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
