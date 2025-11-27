namespace AutoPartsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string Manufacturer { get; set; }

        public required Category Category { get; set; }
        public ICollection<PurchaseHistory> PurchaseHistory { get; set; }

        // Связи 1:1 для специализированных товаров
        public Tire Tire { get; set; }
        public BrakePad BrakePad { get; set; }
        public Battery Battery { get; set; }
        public Wiper Wiper { get; set; }
        public SparkPlug SparkPlug { get; set; }
        public MotorOil MotorOil { get; set; }
    }
}
