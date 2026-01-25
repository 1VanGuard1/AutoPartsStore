using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public HomeController(AutoPartsStoreContext context)
        {
            _context = context;
        }
        public IActionResult Index(string query, string sort)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(p => p.ProductName.Contains(query));
            }

            products = sort switch
            {
                "name_asc" => products.OrderBy(p => p.ProductName),
                "name_desc" => products.OrderByDescending(p => p.ProductName),
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                _ => products.OrderByDescending(p => p.ProductID) // или тво€ логика Ђпопул€рныхї
            };

            var model = new HomeIndexViewModel
            {
                Products = products.ToList()
            };

            return View(model);
        }


    }

}
