using AutoPartsStore.Data;
using AutoPartsStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public AdminController(AutoPartsStoreContext context)
        {
            _context = context;
        }

        // ◀ 1. Список категорий
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // ◀ 2. Товары выбранной категории
        public async Task<IActionResult> Products(int categoryId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

            if (category == null)
                return NotFound();

            var products = await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .ToListAsync();

            ViewBag.Category = category;
            return View(products);
        }

        // ◀ 3. Создать товар
        public IActionResult CreateProduct(int categoryId)
        {
            var p = new Product { CategoryID = categoryId };
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products", new { categoryId = product.CategoryID });
        }

        // ◀ 4. Редактировать товар
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products", new { categoryId = product.CategoryID });
        }

        // ◀ 5. Удалить товар
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
                return NotFound();

            int categoryId = product.CategoryID;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products", new { categoryId });
        }
    }
}
