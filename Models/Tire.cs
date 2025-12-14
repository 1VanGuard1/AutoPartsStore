using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Tire
    {
        [Key]
        public int ProductID { get; set; } //
        public int Width { get; set; } // 
        public int Height { get; set; } // 
        public int Diameter { get; set; } // 
        public required string Season { get; set; } // 
        public required string Type { get; set; } // 
        public Product Product { get; set; }
    }
}
