using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Battery
    {
        [Key]
        public int ProductID { get; set; }
        public int Capacity { get; set; }
        public string Polarity { get; set; }
        public int? WarrantyPeriod { get; set; }
        public decimal? Voltage { get; set; }

        public Product Product { get; set; }
    }
}
