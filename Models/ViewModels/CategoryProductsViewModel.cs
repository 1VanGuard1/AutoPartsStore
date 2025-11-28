using AutoPartsStore.Models;
using System.Collections.Generic;

namespace AutoPartsStore.Models.ViewModels
{
    public class CategoryProductsViewModel
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}
