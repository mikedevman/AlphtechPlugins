using Microsoft.AspNetCore.Mvc;
using AlphtechPlugins.Models;
using AlphtechPlugins.Services;

namespace AlphtechPlugins.Controllers;

public class CartController : Controller
{
    private readonly IPluginCatalogService _catalog;
    private readonly ICartService _cart;

    public CartController(IPluginCatalogService catalog, ICartService cart)
    {
        _catalog = catalog;
        _cart = cart;
    }

    public IActionResult Index()
    {
        ViewBag.CartCount = _cart.GetCount();
        ViewBag.Total = _cart.GetTotal();
        return View(_cart.GetItems());
    }

    [HttpPost]
    public IActionResult Add(string productId)
    {
        var plugin = _catalog.GetById(productId);
        if (plugin != null)
        {
            _cart.AddItem(new CartItem
            {
                ProductId = plugin.Id,
                ProductName = plugin.Name,
                Price = plugin.Price,
                ImageUrl = plugin.ImageUrl
            });
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult AddAndStay(string productId, string returnUrl)
    {
        var plugin = _catalog.GetById(productId);
        if (plugin != null)
        {
            _cart.AddItem(new CartItem
            {
                ProductId = plugin.Id,
                ProductName = plugin.Name,
                Price = plugin.Price,
                ImageUrl = plugin.ImageUrl
            });
        }
        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Remove(string productId)
    {
        _cart.RemoveItem(productId);
        return RedirectToAction("Index");
    }
}
