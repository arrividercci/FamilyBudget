using Aspose.Cells;
using FamilyBudget.WebServer.ReportService.Data.Models;

namespace FamilyBudget.WebServer.ReportService.Services.Excel
{
    public class UserExcelService : IExcelService<UserDto>
    {
        public Workbook GenerateReport(UserDto model)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            sheet.Cells["A1"].PutValue("Name");
            sheet.Cells["B1"].PutValue("Price");
            sheet.Cells["C1"].PutValue("Date");
            int row = 2;
            foreach (var purchase in model.Purchases)
            {
                sheet.Cells[$"A{row}"].PutValue(purchase.Name);
                sheet.Cells[$"B{row}"].PutValue(purchase.Price);
                sheet.Cells[$"C{row}"].PutValue(purchase.Date.ToShortDateString());
                row++;
            }
            return wb;
        }
    }
}
