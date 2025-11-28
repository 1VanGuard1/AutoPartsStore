namespace AutoPartsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public required Category Category { get; set; }
        public ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
