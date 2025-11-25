namespace AutoPartsStore.Models
{
    public class Wipers
    {
        public int ProductID { get; set; }
        public double Length { get; set; }
        public required string MountType { get; set; }
        public required string Season { get; set; }
        public required string Material { get; set; }
    }
}
