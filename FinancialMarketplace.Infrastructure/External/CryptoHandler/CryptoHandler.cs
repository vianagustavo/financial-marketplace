using FinancialMarketplace.Application.Contracts.External;

namespace FinancialMarketplace.Infrastructure.External;

public class CryptoHandler : ICryptoHandler
{
    public string Encrypt(string payload)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(payload, salt);
    }
    public bool Compare(string payload, string hashed)
    {
        return BCrypt.Net.BCrypt.Verify(payload, hashed);
    }
}
