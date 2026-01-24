using AutoPartsStore.Data;
using AutoPartsStore.Helpers;
using AutoPartsStore.Models;
using AutoPartsStore.Models.ViewModels.Admin;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AutoPartsStore.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AutoPartsStoreContext _context;

        public AdminController(AutoPartsStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

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

            // определяем контроллер по названию категории
            ViewBag.ControllerName = category.CategoryName switch
            {
                "Batteries" => "Battery",
                "Tires" => "Tire",
                "Motor Oils" => "MotorOil",
                "Brake Pads" => "BrakePad",
                "Wipers" => "Wiper",
                "Spark Plugs" => "SparkPlug",
                _ => "Admin"
            };

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
        public async Task<IActionResult> AddBattery(BatteryViewModel vm, IFormFile image)
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

        // ▶ Страница с выбором периода
        [HttpGet]
        public IActionResult SalesReport()
        {
            return View();
        }

        // ▶ Экспорт отчёта о продажах в Excel
        [HttpPost]
        public async Task<IActionResult> ExportSalesReportToExcel(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue)
                startDate = DateTime.MinValue;
            if (!endDate.HasValue)
                endDate = DateTime.MaxValue;

            // Берём продажи за период
            var purchases = await _context.PurchaseHistory
                .Include(ph => ph.User)
                .Include(ph => ph.Product)
                .ThenInclude(p => p.Category)
                .Where(ph => ph.PurchaseDate >= startDate && ph.PurchaseDate <= endDate)
                .OrderByDescending(ph => ph.PurchaseDate)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Отчёт о продажах");

                // Заголовки
                ws.Cell("A1").Value = "Пользователь ID";
                ws.Cell("B1").Value = "ФИО клиента";
                ws.Cell("C1").Value = "Email";
                ws.Cell("D1").Value = "Товар ID";
                ws.Cell("E1").Value = "Название товара";
                ws.Cell("F1").Value = "Категория";
                ws.Cell("G1").Value = "Количество";
                ws.Cell("H1").Value = "Сумма (руб.)";
                ws.Cell("I1").Value = "Дата покупки";

                var header = ws.Range("A1:I1");
                header.Style.Font.Bold = true;
                header.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                header.Style.Font.FontColor = XLColor.White;
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 2;
                decimal totalAmount = 0;

                foreach (var ph in purchases)
                {
                    ws.Cell(row, 1).Value = ph.UserID;
                    ws.Cell(row, 2).Value = ph.User?.FullName ?? "Не указано";
                    ws.Cell(row, 3).Value = ph.User?.Email ?? "Не указано";
                    ws.Cell(row, 4).Value = ph.ProductID;
                    ws.Cell(row, 5).Value = ph.Product?.ProductName ?? "Удалено";
                    ws.Cell(row, 6).Value = ph.Product?.Category?.CategoryName ?? "-";
                    ws.Cell(row, 7).Value = ph.Quantity;
                    ws.Cell(row, 8).Value = ph.TotalPrice;
                    ws.Cell(row, 9).Value = ph.PurchaseDate;   

                    totalAmount += ph.TotalPrice;
                    row++;
                }

                // ИТОГО
                ws.Cell(row, 6).Value = "ИТОГО:";
                ws.Cell(row, 6).Style.Font.Bold = true;
                ws.Cell(row, 8).Value = totalAmount;
                ws.Cell(row, 8).Style.Font.Bold = true;
                ws.Cell(row, 8).Style.Fill.BackgroundColor = XLColor.LightYellow;

                ws.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var fileName = $"SalesReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";

                return File(
                    stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
        }

    }
}
