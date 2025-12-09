using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers
{
    public class BatteryController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public BatteryController(AutoPartsStoreContext context)
        {
            _context = context;
        }

        // ------------------------ ADD ------------------------

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = new SelectList(
                await _context.Categories.ToListAsync(),
                "CategoryID",
                "CategoryName"
            );

            return View("AddBattery");  // чтобы View назывался AddBattery.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBatteryViewModel vm, IFormFile image)
        {
            var product = new Product
            {
                CategoryID = vm.CategoryID,
                ProductName = vm.ProductName,
                Price = vm.Price,
                Manufacturer = vm.Manufacturer,
                Description = vm.Description
            };

            // изображение
            if (image != null && image.Length > 0)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            // добавляем товар
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // добавляем аккумулятор, связанный по ProductID
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

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ EDIT ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.Battery == null)
                return NotFound();

            return View("EditBattery", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .FirstOrDefaultAsync(p => p.ProductID == model.ProductID);

            if (product == null)
                return NotFound();

            // обновляем Product
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Manufacturer = model.Manufacturer;
            product.Description = model.Description;

            // новое изображение?
            if (image != null && image.Length > 0)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            // обновляем Battery
            product.Battery.Capacity = model.Battery.Capacity;
            product.Battery.Polarity = model.Battery.Polarity;
            product.Battery.WarrantyPeriod = model.Battery.WarrantyPeriod;
            product.Battery.Voltage = model.Battery.Voltage;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ DELETE ------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Battery)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
    }
}
