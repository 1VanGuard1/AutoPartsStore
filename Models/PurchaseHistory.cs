namespace AutoPartsStore.Models
{
    public class PurchaseHistory
    {
        public int UserID {get; set;}
        public int ProductID{ get; set; }
        public DateTime PurchaseDate {get; set;}
        public int Quantity {get; set;}
        public double TotalPrice {get; set;}
    }
}
