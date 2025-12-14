namespace AutoPartsStore.Models.ViewModels.Admin
{
    public class TireViewModel
    {
        public int CategoryID { get; set; }

        // Общие поля Product
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string? Description { get; set; }

        // Поля Tire
        public int Width { get; set; }
        public int Height { get; set; }
        public int Diameter { get; set; }
        public string Season { get; set; }
        public string Type { get; set; }
    }
}
