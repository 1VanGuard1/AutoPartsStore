using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
    }
}
