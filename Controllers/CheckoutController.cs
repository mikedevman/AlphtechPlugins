using Microsoft.AspNetCore.Mvc;
using AlphtechPlugins.Models;
using AlphtechPlugins.Services;

namespace AlphtechPlugins.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cart;
    private readonly ILicenseService _license;

    public CheckoutController(ICartService cart, ILicenseService license)
    {
        _cart = cart;
        _license = license;
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

        _cart.Clear();
        return View("Confirmation", order);
    }
}
