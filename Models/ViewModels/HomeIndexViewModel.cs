using System.Collections.Generic;

namespace AutoPartsStore.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
