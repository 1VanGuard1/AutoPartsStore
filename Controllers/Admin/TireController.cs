using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers.Admin
{
    public class TireController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public TireController(AutoPartsStoreContext context)
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

            return View("AddTire.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add(TireViewModel vm, IFormFile image)
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
          
            var Tire = new Tire
            {
                ProductID = product.ProductID,
                Width = vm.Width,
                Height = vm.Height,
                Diameter = vm.Diameter,
                Season = vm.Season,
                Type = vm.Type
            };

            _context.Tires.Add(Tire);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ EDIT ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Tire)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.Tire == null)
                return NotFound();

            return View("~/Views/Admin/Tire/EditTire.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.Tire)
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

            // обновляем Tire
            
            product.Tire.Width = model.Tire.Width;
            product.Tire.Height = model.Tire.Height;
            product.Tire.Diameter = model.Tire.Diameter;
            product.Tire.Season = model.Tire.Season;
            product.Tire.Type = model.Tire.Type;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ DELETE ------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Tire)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
    }
}
