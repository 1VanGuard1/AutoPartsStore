using AutoPartsStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly AutoPartsStoreContext _context;

        public CategoryMenuViewComponent(AutoPartsStoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}
