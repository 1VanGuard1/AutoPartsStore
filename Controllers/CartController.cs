using AutoPartsStore.Data;
using AutoPartsStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CartController : Controller
{
    private readonly AutoPartsStoreContext _context;
    private const string CartCookie = "cart";

    public CartController(AutoPartsStoreContext context)
    {
        _context = context;
    }

    // Читает корзину из куки
    private List<CartItem> GetCart()
    {
        var cookie = Request.Cookies[CartCookie];
        if (string.IsNullOrEmpty(cookie))
            return new List<CartItem>();

        return JsonConvert.DeserializeObject<List<CartItem>>(cookie) ?? new List<CartItem>();
    }

    // Сохраняет корзину в куки
    private void SaveCart(List<CartItem> cart)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(30),
            HttpOnly = false,
        };

        Response.Cookies.Append(CartCookie, JsonConvert.SerializeObject(cart), options);
    }

    // Добавление товара
    [HttpPost]
    public IActionResult Add(int productId)
    {
        var cart = GetCart();

        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item == null)
            cart.Add(new CartItem { ProductId = productId, Quantity = 1 });
        else
            item.Quantity++;

        SaveCart(cart);
        return Json(new { ok = true });
    }

    // Страница корзины
    public IActionResult Index()
    {
        var cart = GetCart();

        var products = _context.Products
            .Where(p => cart.Select(c => c.ProductId).Contains(p.ProductID))
            .ToList();

        var vm = cart.Select(cartItem => new CartViewModelItem
        {
            Product = products.First(p => p.ProductID == cartItem.ProductId),
            Quantity = cartItem.Quantity
        }).ToList();

        return View(vm);
    }

    // Удаление
    [HttpPost]
    public IActionResult Remove(int productId)
    {
        var cart = GetCart();
        cart = cart.Where(c => c.ProductId != productId).ToList();
        SaveCart(cart);
        return Json(new { ok = true });
    }
}

public class CartItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CartViewModelItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
