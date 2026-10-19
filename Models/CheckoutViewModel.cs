using System.ComponentModel.DataAnnotations;

namespace AlphtechPlugins.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Platform { get; set; } = "Windows";

    [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
    public List<CartItem> CartItems { get; set; } = new();
    public decimal Total { get; set; } = 0.00m;
}
