using ErrorOr;

namespace FinancialMarketplace.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Product
    {
        public static Error BadRequest => Error.Validation(
        code: "Product.BadRequest",
        description: "Produto já cadastrado");
    }
}
