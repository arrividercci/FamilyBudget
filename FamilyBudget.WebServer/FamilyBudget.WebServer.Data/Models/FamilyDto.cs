namespace FamilyBudget.WebServer.Data.Models
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public string? Name { get; set; }
        public List<UserForFamilyDto> Users { get; set; } = default!;
        public List<PurchaseForFamilyDto> Purchases { get; set; } = default!;
    }
}
