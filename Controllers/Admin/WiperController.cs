using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.Controllers.Admin
{
    public class WiperController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public WiperController(AutoPartsStoreContext context)
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

            return View("AddWiper.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add(WiperViewModel vm, IFormFile image)
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
        var Wiper = new Wiper
            {
                ProductID = product.ProductID,
                Length = vm.Length,
                MountType = vm.MountType,  
                Season = vm.Season,
                Material = vm.Material
        };

            _context.Wipers.Add(Wiper);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ EDIT ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Wiper)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.Wiper == null)
                return NotFound();

            return View("~/Views/Admin/Wiper/EditWiper.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.Wiper)
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

            // обновляем Wiper
                       
            product.Wiper.Length = model.Wiper.Length;
            product.Wiper.MountType = model.Wiper.MountType;
            product.Wiper.Season = model.Wiper.Season;
            product.Wiper.Material = model.Wiper.Material;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ DELETE ------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Wiper)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
    }
}
