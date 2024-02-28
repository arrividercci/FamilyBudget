using Aspose.Cells;

namespace FamilyBudget.WebServer.ReportService.Services.Excel
{
    public interface IExcelService<T> where T : class
    {
        public Workbook GenerateReport(T model);
    }
}
