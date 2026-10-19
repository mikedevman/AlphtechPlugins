using System.Security.Cryptography;
using System.Text;

namespace AlphtechPlugins.Services;

public interface ILicenseService
{
    string GenerateKey(string productId, string email);
}

public class LicenseService : ILicenseService
{
    public string GenerateKey(string productId, string email)
    {
        var seed = $"{productId}-{email}-{DateTime.UtcNow.Ticks}";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        var hex = Convert.ToHexString(hash)[..16].ToUpper();
        return $"ALPH-{hex[..4]}-{hex[4..8]}-{hex[8..12]}-{hex[12..16]}";
    }
}
