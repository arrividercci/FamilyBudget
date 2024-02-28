namespace FamilyBudget.WebServer.ReportService.Data.Models
{
    public class UserDto
    {
        public string? Id { get; set; }
        public double Balance { get; set; }
        public string? Email { get; set; }
        public List<PurchaseDto> Purchases { get; set; } = default!;
    }
}
