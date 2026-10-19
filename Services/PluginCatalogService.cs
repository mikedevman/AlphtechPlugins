namespace AlphtechPlugins.Services;

using AlphtechPlugins.Data;
using AlphtechPlugins.Models;
using Microsoft.EntityFrameworkCore;

public interface IPluginCatalogService
{
    List<PluginProduct> GetAll();
    List<PluginProduct> GetByCategory(string category);
    PluginProduct? GetById(string id);
    List<string> GetCategories();
}

public class PluginCatalogService : IPluginCatalogService
{
    private readonly AlphtechDbContext _db;

    public PluginCatalogService(AlphtechDbContext db)
    {
        _db = db;
    }

    public List<PluginProduct> GetAll() =>
        _db.Products.AsNoTracking().ToList();

    public List<PluginProduct> GetByCategory(string category) =>
        string.IsNullOrEmpty(category) || category == "All"
            ? _db.Products.AsNoTracking().ToList()
            : _db.Products.AsNoTracking().Where(p => p.Category == category).ToList();

    public PluginProduct? GetById(string id) =>
        _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

    public List<string> GetCategories() =>
        _db.Products.AsNoTracking().Select(p => p.Category).Distinct().ToList();
}
