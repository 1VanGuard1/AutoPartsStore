using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Models.ViewModels.Admin
{
    public class AddBatteryViewModel
    {
        // Общие поля товара
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string? Description { get; set; }

        // Поля аккумулятора
        public int Capacity { get; set; }
        public string Polarity { get; set; }
        public int? WarrantyPeriod { get; set; }
        public decimal? Voltage { get; set; }
    }
}
