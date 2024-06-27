using ErrorOr;

namespace FinancialMarketplace.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Account
    {
        public static Error InsufficientFunds => Error.Validation(
        code: "Accounts.InsufficientFunds",
        description: "Saldo indisponível");
    }
}
