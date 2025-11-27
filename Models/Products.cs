namespace AutoPartsStore.Models
{
    public class Products
    {
        public int ProductID { get; set; } //
        public int CategoryID { get; set; } // 
        public required string ProductName { get; set; } // 
        public double Price { get; set; } // Гарантийный период
        public int Manufacturer { get; set; } // Производитель
    }
}
