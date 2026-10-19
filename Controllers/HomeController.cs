using Microsoft.AspNetCore.Mvc;
using AlphtechPlugins.Services;

namespace AlphtechPlugins.Controllers;

public class HomeController : Controller
{
    private readonly IPluginCatalogService _catalog;
    private readonly ICartService _cart;

    public HomeController(IPluginCatalogService catalog, ICartService cart)
    {
        _catalog = catalog;
        _cart = cart;
    }

    public IActionResult Index(string? category)
    {
        ViewBag.Categories = _catalog.GetCategories();
        ViewBag.SelectedCategory = category;
        ViewBag.CartCount = _cart.GetCount();
        var plugins = _catalog.GetByCategory(category ?? "All");
        return View(plugins);
    }
}
