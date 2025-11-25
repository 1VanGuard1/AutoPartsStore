namespace AutoPartsStore.Models
{
    public class Products
    {
        public int ProductID { get; set; } //
        public double CategoryID { get; set; } // 
        public required string ProductName { get; set; } // 
        public double Price { get; set; } // Гарантийный период
        public int Manufacturer { get; set; } // Производитель
    }
}
