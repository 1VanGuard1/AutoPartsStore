using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Tire
    {
        [Key]
        public int ProductID { get; set; } //
        public double Width { get; set; } // 
        public double Height { get; set; } // 
        public double Diameter { get; set; } // 
        public required string Season { get; set; } // 
        public required string Type { get; set; } // 
        public Product Product { get; set; }
    }
}
