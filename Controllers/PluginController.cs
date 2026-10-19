using Microsoft.AspNetCore.Mvc;
using AlphtechPlugins.Services;

namespace AlphtechPlugins.Controllers;

public class PluginController : Controller
{
    private readonly IPluginCatalogService _catalog;
    private readonly ICartService _cart;

    public PluginController(IPluginCatalogService catalog, ICartService cart)
    {
        _catalog = catalog;
        _cart = cart;
    }

    public IActionResult Details(string id)
    {
        var plugin = _catalog.GetById(id);
        if (plugin == null) return NotFound();
        ViewBag.CartCount = _cart.GetCount();
        ViewBag.InCart = _cart.Contains(id);
        return View(plugin);
    }
}
