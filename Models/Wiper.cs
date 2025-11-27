namespace AutoPartsStore.Models
{
    public class Wiper
    {
        public int ProductID { get; set; }
        public int Length { get; set; }
        public string MountType { get; set; }
        public string Season { get; set; }
        public string Material { get; set; }

        public Product Product { get; set; }
    }
}
