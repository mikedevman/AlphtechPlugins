namespace AlphtechPlugins.Models;

public class PluginProduct
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.00m;
    public string Formats { get; set; } = "VST3, AU, AAX";
    public string Platforms { get; set; } = "Windows, macOS";
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public double Rating { get; set; } = 4.5;
    public int Downloads { get; set; } = 0;
    public int DisplayOrder { get; set; } = 0;
    public List<string> Features { get; set; } = new();
    public string SampleRate { get; set; } = "Up to 192kHz";
    public string BitDepth { get; set; } = "64-bit floating point";
    public string Latency { get; set; } = "Zero latency";
    public bool IsFree => Price == 0.00m;
    public string PriceDisplay => IsFree ? "FREE" : $"${Price:F2}";
}
