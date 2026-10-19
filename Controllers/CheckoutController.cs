using Microsoft.AspNetCore.Mvc;
using AlphtechPlugins.Data;
using AlphtechPlugins.Models;
using AlphtechPlugins.Services;

namespace AlphtechPlugins.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cart;
    private readonly ILicenseService _license;
    private readonly AlphtechDbContext _db;

    public CheckoutController(ICartService cart, ILicenseService license, AlphtechDbContext db)
    {
        _cart = cart;
        _license = license;
        _db = db;
    }

    public IActionResult Index()
    {
        if (_cart.GetCount() == 0)
            return RedirectToAction("Index", "Home");

        ViewBag.CartCount = _cart.GetCount();
        var model = new CheckoutViewModel
        {
            CartItems = _cart.GetItems(),
            Total = _cart.GetTotal()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Process(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.CartItems = _cart.GetItems();
            model.Total = _cart.GetTotal();
            ViewBag.CartCount = _cart.GetCount();
            return View("Index", model);
        }

        var order = new Order
        {
            OrderId = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            CustomerName = model.Name,
            CustomerEmail = model.Email,
            Platform = model.Platform,
            OrderDate = DateTime.UtcNow,
            Total = _cart.GetTotal(),
            Items = _cart.GetItems().Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                ProductName = ci.ProductName,
                Price = ci.Price,
                LicenseKey = _license.GenerateKey(ci.ProductId, model.Email)
            }).ToList()
        };

        // Increment download counters on purchased plugins
        foreach (var item in order.Items)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product != null)
            {
                product.Downloads++;
            }
        }

        // Persist order and items into SQLite database
        _db.Orders.Add(order);
        _db.SaveChanges();

        _cart.Clear();
        return View("Confirmation", order);
    }
}
