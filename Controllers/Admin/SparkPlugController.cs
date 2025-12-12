using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers.Admin
{
    public class SparkPlugController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public SparkPlugController(AutoPartsStoreContext context)
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

            return View("AddSparkPlug.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add(SparkPlugViewModel vm, IFormFile image)
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

            // добавляем SparkPlug, связанный по ProductID
            var SparkPlug = new SparkPlug
            {
                ProductID = product.ProductID,
                ThreadSize = vm.ThreadSize,
                ElectrodeMaterial = vm.ElectrodeMaterial,
                HeatRange = vm.HeatRange,
                Gap = vm.Gap
            };

            _context.SparkPlugs.Add(SparkPlug);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ EDIT ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.SparkPlug)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.SparkPlug == null)
                return NotFound();

            return View("~/Views/Admin/SparkPlug/EditSparkPlug.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.SparkPlug)
                .FirstOrDefaultAsync(p => p.ProductID == model.ProductID);

            if (product == null)
                return NotFound();

            // обновляем Product
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Manufacturer = model.Manufacturer;
            product.Description = model.Description;

            // изображение
            if (image != null && image.Length > 0)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                product.ImageData = ms.ToArray();
            }

            // обновляем SparkPlug

            product.SparkPlug.Gap = model.SparkPlug.Gap;
            product.SparkPlug.HeatRange = model.SparkPlug.HeatRange;
            product.SparkPlug.ElectrodeMaterial = model.SparkPlug.ElectrodeMaterial;
            product.SparkPlug.ThreadSize = model.SparkPlug.ThreadSize;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ DELETE ------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.SparkPlug)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
    }
}
