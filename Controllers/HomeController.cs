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

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            var products = await _context.Products
                .Include(p => p.Category)
                .Take(20)
                .ToListAsync();

            var vm = new HomeIndexViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(vm);
        }
    }
}
