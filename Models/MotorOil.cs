namespace AutoPartsStore.Models
{
    public class MotorOil
    {
        public int ProductID { get; set; }
        public string Viscosity { get; set; }
        public decimal? Volume { get; set; }
        public string OilType { get; set; }
        public string ManufacturerCountry { get; set; }

        public Product Product { get; set; }
    }
}
