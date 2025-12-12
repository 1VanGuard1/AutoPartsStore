namespace AutoPartsStore.Models.ViewModels.Admin
{
    public class BrakePadViewModel
    {
        // Общие поля товара
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string? Description { get; set; }

        public required string PadType { get; set; } // Тип колодки
        public required string Material { get; set; } // Материал
        public required string Compatibility { get; set; } // Совместимость
        public decimal Thickness { get; set; } // Thickness
    }
}
