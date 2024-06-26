namespace FinancialMarketplace.Application.Contracts.External;

public interface ICryptoHandler
{
    string Encrypt(string payload);
    bool Compare(string payload, string hashed);
}