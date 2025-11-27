using AutoPartsStore.Data;
using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly AutoPartsStoreContext _context;

    public CategoryController(AutoPartsStoreContext context)
    {
        _context = context;
    }

    public IActionResult Products(int id)
    {
        var category = _context.Categories
            .FirstOrDefault(c => c.CategoryID == id);

        if (category == null)
            return NotFound();

        var products = _context.Products
            .Where(p => p.CategoryID == id)
            .ToList();

        var vm = new CategoryProductsViewModel
        {
            Category = category,
            Products = products
        };

        return View(vm);
    }
}
