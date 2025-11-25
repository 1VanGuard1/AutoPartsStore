namespace AutoPartsStore.Models
{
    public class MotorOils
    {
        public int ProductID { get; set; }
        public double Viscosity { get; set; } // Вязкость
        public double Volume { get; set; }
        public required string OilType { get; set; }
        public required string ManufacturerCountry { get; set; }
    }
}
