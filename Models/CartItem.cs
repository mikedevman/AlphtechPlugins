namespace AlphtechPlugins.Models;

public class CartItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.00m;
    public string ImageUrl { get; set; } = string.Empty;
}
