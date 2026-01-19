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

            return View(vm);
        }


        // ▶ Страница одного товара
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Battery)
                .Include(p => p.Tire)
                .Include(p => p.MotorOil)
                .Include(p => p.BrakePad)
                .Include(p => p.Wiper)
                .Include(p => p.SparkPlug)
                .FirstOrDefaultAsync(p => p.ProductID == productId);
            Console.WriteLine(product);
            if (product == null)
                return NotFound();

            return View(product);
        }
        public async Task<IActionResult> Search(string query, int? categoryId)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View(new List<Product>());

            var searchLower = query.ToLower().Trim();

            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(p => p.CategoryID == categoryId.Value);

            var products = await productsQuery
                .Where(p => p.ProductName.ToLower().Contains(searchLower))
                .OrderByDescending(p => p.ProductName.ToLower().StartsWith(searchLower))
                .ThenBy(p => p.ProductName)
                .ToListAsync();

            ViewBag.SearchQuery = query;
            ViewBag.CategoryId = categoryId;

            return View(products);
        }
    }
}
