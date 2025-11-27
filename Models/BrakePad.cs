using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class BrakePad
    {
        [Key]
        public int ProductID { get; set; } //
        public required string PadType { get; set; } // Тип колодки
        public required string Material { get; set; } // Материал
        public required string Compatibility { get; set; } // Совместимость
        public int Thickness { get; set; } // Thickness
        public Product Product { get; set; }
    }
}
