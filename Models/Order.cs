namespace AlphtechPlugins.Models;

public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string Platform { get; set; } = "Windows";
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; } = 0.00m;
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.00m;
    public string LicenseKey { get; set; } = string.Empty;
}
