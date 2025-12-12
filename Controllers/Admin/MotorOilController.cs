using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoPartsStore.Controllers.Admin
{
    public class MotorOilController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public MotorOilController(AutoPartsStoreContext context)
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

            return View("AddMotorOil.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add(MotorOilViewModel vm, IFormFile image)
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
            var MotorOil = new MotorOil
            {
                ProductID = product.ProductID,
                Viscosity = vm.Viscosity,
                Volume = vm.Volume,
                OilType = vm.OilType,
                ManufacturerCountry = vm.ManufacturerCountry
            };

            _context.MotorOils.Add(MotorOil);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ EDIT ------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.MotorOil)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null || product.MotorOil == null)
                return NotFound();

            return View("~/Views/Admin/MotorOil/EditMotorOil.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = await _context.Products
                .Include(p => p.MotorOil)
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

            // обновляем MotorOil

            product.MotorOil.Viscosity = model.MotorOil.Viscosity;
            product.MotorOil.Volume = model.MotorOil.Volume;
            product.MotorOil.OilType = model.MotorOil.OilType;
            product.MotorOil.ManufacturerCountry = model.MotorOil.ManufacturerCountry;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        // ------------------------ DELETE ------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.MotorOil)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
    }
}
