namespace AutoPartsStore.Models.ViewModels.Admin
{
    public class WiperViewModel
    {
        // Общие поля товара
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string? Description { get; set; }

        public int Length { get; set; }
        public string MountType { get; set; }
        public string Season { get; set; }
        public string Material { get; set; }

    }
}
