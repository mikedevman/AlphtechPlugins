namespace AlphtechPlugins.Services;

using AlphtechPlugins.Models;

public interface ICartService
{
    void AddItem(CartItem item);
    void RemoveItem(string productId);
    List<CartItem> GetItems();
    decimal GetTotal();
    int GetCount();
    void Clear();
    bool Contains(string productId);
}

public class CartService : ICartService
{
    private readonly List<CartItem> _items = new();

    public void AddItem(CartItem item)
    {
        if (!_items.Any(i => i.ProductId == item.ProductId))
            _items.Add(item);
    }

    public void RemoveItem(string productId) =>
        _items.RemoveAll(i => i.ProductId == productId);

    public List<CartItem> GetItems() => _items;
    public decimal GetTotal() => _items.Sum(i => i.Price);
    public int GetCount() => _items.Count;
    public void Clear() => _items.Clear();
    public bool Contains(string productId) => _items.Any(i => i.ProductId == productId);
}
