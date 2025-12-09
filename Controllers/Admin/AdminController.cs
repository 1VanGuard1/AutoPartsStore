using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //public async Task<IActionResult> Index()
        //{
        //    var products = await _context.Products
        //        .Include(p => p.Category)
        //        .ToListAsync();

        //    return View(products);
        //}

        // Список категорий
        //public async Task<IActionResult> Categories()
        //{
        //    var categories = await _context.Categories.ToListAsync();
        //    return View(categories);
        //}
        
        // new
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Category(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            var products = await _context.Products
                .Where(p => p.CategoryID == id)
                .Include(p => p.Category)
                .ToListAsync();

            ViewBag.Category = category;

            return View(products);
        }




        // Товары выбранной категории
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

        [HttpGet]
        public async Task<IActionResult> AddBattery()
        {
            ViewBag.Categories = new SelectList(
                await _context.Categories.ToListAsync(),
                "CategoryID",
                "CategoryName"
            );

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBattery(AddBatteryViewModel vm, IFormFile image)
        {
            var product = new Product
            {
                CategoryID = vm.CategoryID,
                ProductName = vm.ProductName,
                Price = vm.Price,
                Manufacturer = vm.Manufacturer,
                Description = vm.Description
            };

            if (image != null && image.Length > 0)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // получаем ProductID

            var battery = new Battery
            {
                ProductID = product.ProductID,
                Capacity = vm.Capacity,
                Polarity = vm.Polarity,
                WarrantyPeriod = vm.WarrantyPeriod,
                Voltage = vm.Voltage
            };

            _context.Batteries.Add(battery);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .Include(p => p.Tire)
                .Include(p => p.MotorOil)
                .Include(p => p.BrakePad)
                .Include(p => p.Wiper)
                .Include(p => p.SparkPlug)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            // АККУМУЛЯТОР
            if (product.Battery != null)
                return RedirectToAction("EditBattery", new { id });

            // ШИНЫ
            if (product.Tire != null)
                return RedirectToAction("EditTire", new { id });

            // МАСЛО
            if (product.MotorOil != null)
                return RedirectToAction("EditMotorOil", new { id });

            // ТОРМОЗНЫЕ КОЛОДКИ
            if (product.BrakePad != null)
                return RedirectToAction("EditBrakePad", new { id });

            // ДВОРНИКИ
            if (product.Wiper != null)
                return RedirectToAction("EditWiper", new { id });

            // СВЕЧИ
            if (product.SparkPlug != null)
                return RedirectToAction("EditSparkPlug", new { id });

            return BadRequest("Тип товара не определён");
        }

        [HttpGet]
        public async Task<IActionResult> EditBattery(int id)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.Battery == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditBattery(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .FirstOrDefaultAsync(p => p.ProductID == model.ProductID);

            if (product == null)
                return NotFound();

            // ОБНОВЛЯЕМ Product
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Manufacturer = model.Manufacturer;
            product.Description = model.Description;

            if (image != null && image.Length > 0)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            //  ОБНОВЛЯЕМ Battery
            product.Battery.Capacity = model.Battery.Capacity;
            product.Battery.Polarity = model.Battery.Polarity;
            product.Battery.WarrantyPeriod = model.Battery.WarrantyPeriod;
            product.Battery.Voltage = model.Battery.Voltage;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

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
