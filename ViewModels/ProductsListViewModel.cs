using AutoPartsStore.Models;

namespace AutoPartsStore.ViewModels
{
    public class ProductsListViewModel
    {
        public List<Products> Products { get; set; }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public int? SelectedCategoryId { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }

}
