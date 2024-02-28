using Aspose.Cells;
using FamilyBudget.WebServer.ReportService.Data.Models;

namespace FamilyBudget.WebServer.ReportService.Services.Excel
{
    public class FamilyExcelService : IExcelService<FamilyDto>
    {
        public Workbook GenerateReport(FamilyDto model)
        {
            var wb = new Workbook();
            var sheet = wb.Worksheets[0];
            sheet.Cells["A1"].PutValue("Member");
            sheet.Cells["B1"].PutValue("Name");
            sheet.Cells["C1"].PutValue("Price");
            sheet.Cells["D1"].PutValue("Date");
            int row = 2;
            foreach (var purchase in model.Purchases)
            {
                sheet.Cells[$"B{row}"].PutValue(purchase.FamilyMemberName);
                sheet.Cells[$"B{row}"].PutValue(purchase.Name);
                sheet.Cells[$"C{row}"].PutValue(purchase.Price);
                sheet.Cells[$"D{row}"].PutValue(purchase.Date.ToShortDateString());
                row++;
            }
            return wb;
        }
    }
}
