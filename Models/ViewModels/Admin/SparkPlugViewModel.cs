namespace AutoPartsStore.Models.ViewModels.Admin
{
    public class SparkPlugViewModel
    {
        // Общие поля товара
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string? Description { get; set; }
        
        public string ThreadSize { get; set; }
        public string ElectrodeMaterial { get; set; }
        public int? HeatRange { get; set; }
        public decimal? Gap { get; set; }
    }
}
