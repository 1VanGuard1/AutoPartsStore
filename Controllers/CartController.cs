using AutoPartsStore.Data;
using AutoPartsStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // Увеличить количество
    [HttpPost]
    public IActionResult Increment(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Quantity++;
            SaveCart(cart);
        }

        return Json(new { ok = true });
    }

    // Уменьшить количество
    [HttpPost]
    public IActionResult Decrement(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
                cart.Remove(item);

            SaveCart(cart);
        }

        return Json(new { ok = true });
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

    // Страница корзины
    public IActionResult Index()
    {
        var cart = GetCart();

        if (!cart.Any())
            return View(new List<CartViewModelItem>());

        var ids = cart.Select(c => c.ProductId).ToList();

        var products = _context.Products
            .Include(p => p.Category)
            .Where(p => ids.Contains(p.ProductID))
            .ToList();

        var vm = cart.Select(cartItem => new CartViewModelItem
        {
            Product = products.First(p => p.ProductID == cartItem.ProductId),
            Quantity = cartItem.Quantity
        }).ToList();

        return View(vm);
    }

    // Оформление заказа (минимальный вариант: просто очистить корзину)
    [HttpPost]
    [HttpPost]
    public IActionResult Checkout(string fullName, string email, string phone)
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            TempData["OrderSuccess"] = "Корзина пуста, нечего оформлять.";
            return RedirectToAction("Index");
        }

        // 1. Ищем пользователя по email
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            user = new User
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phone,
                Role = "user",
                // Специальное значение, чтобы отличать “покупателей без логина”
                PasswordHash = "ORDER_ONLY",
                PurchaseHistory = new List<PurchaseHistory>()
            };

            _context.Users.Add(user);
            _context.SaveChanges(); // чтобы появился UserID
        }

        // 2. Готовим покупки
        var productIds = cart.Select(c => c.ProductId).ToList();
        var products = _context.Products
            .Where(p => productIds.Contains(p.ProductID))
            .ToList();

        var now = DateTime.UtcNow;

        foreach (var cartItem in cart)
        {
            var product = products.First(p => p.ProductID == cartItem.ProductId);

            var record = new PurchaseHistory
            {
                UserID = user.UserID,
                ProductID = product.ProductID,
                PurchaseDate = now,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.Quantity * product.Price
            };

            _context.PurchaseHistory.Add(record);
        }

        _context.SaveChanges();

        SaveCart(new List<CartItem>());
        TempData["OrderSuccess"] = "Заказ успешно оформлен! Наш менеджер свяжется с вами.";
        return RedirectToAction("Index");
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
