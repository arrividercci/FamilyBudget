namespace FamilyBudget.WebServer.Data.Models
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
    }
}
