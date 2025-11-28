using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public ProductsController(AutoPartsStoreContext context)
        {
            _context = context;
        }


        // ▶ Страница категории
        public async Task<IActionResult> Category(int categoryId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

            if (category == null)
                return NotFound();

            var products = await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .ToListAsync();

            var vm = new CategoryProductsViewModel
            {
                Category = category,
                Products = products
            };

            return View("Category", vm);
        }


        // ▶ Страница одного товара
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
