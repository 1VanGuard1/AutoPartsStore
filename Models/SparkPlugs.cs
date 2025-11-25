namespace AutoPartsStore.Models
{
    public class SparkPlugs
    {
        public int ProductID { get; set; }
        public int ThreadSize { get; set; }
        public required string ElectrodeMaterial { get; set; }
        public double HeatRange { get; set; }
        public double Gap { get; set; }
    }
}
