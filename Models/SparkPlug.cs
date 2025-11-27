namespace AutoPartsStore.Models
{
    public class SparkPlug
    {
        public int ProductID { get; set; }
        public string ThreadSize { get; set; }
        public string ElectrodeMaterial { get; set; }
        public int? HeatRange { get; set; }
        public decimal? Gap { get; set; }

        public Product Product { get; set; }
    }
}
