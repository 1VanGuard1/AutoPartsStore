namespace AutoPartsStore.Models
{
    public class Batteries
    {
        public int ProductID { get; set; } //
        public double Capacity { get; set; } // Вместимость
        public required string Polarity { get; set; } // Полярность
        public int WarrantyPeriod { get; set; } // Гарантийный период
        public int Voltage { get; set; } // напряжение
    }
}
