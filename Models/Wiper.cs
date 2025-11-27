using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Wiper
    {
        [Key]
        public int ProductID { get; set; }
        public int Length { get; set; }
        public string MountType { get; set; }
        public string Season { get; set; }
        public string Material { get; set; }

        public Product Product { get; set; }
    }
}
