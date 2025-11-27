namespace AutoPartsStore.Models
{
    public class PurchaseHistory
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
