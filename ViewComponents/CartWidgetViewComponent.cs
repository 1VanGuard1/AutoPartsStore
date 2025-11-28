using AutoPartsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.ViewComponents
{
    public class CartWidgetViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        {
            return View("_ProductCard", product);
        }
    }
}
