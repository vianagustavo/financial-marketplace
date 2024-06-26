namespace FinancialMarketplace.Application.Contracts.External;

public interface IEmailProvider
{
    Task Send(string to, string subject, string body);
}