using AutoPartsStore.Data;
using AutoPartsStore.Models;
using AutoPartsStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;
    private const int PageSize = 20;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Products?page=1&categoryId=3
    public async Task<IActionResult> Index(int page = 1, int? categoryId = null)
    {
        IQueryable<Product> query = _context.Products
            .Include(p => p.Category);

        // Фильтрация по категории
        if (categoryId != null)
            query = query.Where(p => p.CategoryId == categoryId);

        // Подсчёт общего количества
        int totalCount = await query.CountAsync();

        // Постраничный вывод
        var products = await query
            .OrderBy(p => p.Id)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // Формируем модель для View
        var viewModel = new ProductsListViewModel
        {
            Products = products,
            CurrentPage = page,
            TotalItems = totalCount,
            PageSize = PageSize,
            SelectedCategoryId = categoryId
        };

        return View(viewModel);
    }
}
