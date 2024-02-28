namespace FamilyBudget.WebServer.Data.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public string? FamilyMemberName { get; set; }
    }
}
