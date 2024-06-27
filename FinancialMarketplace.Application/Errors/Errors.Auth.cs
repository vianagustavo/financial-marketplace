using ErrorOr;

namespace FinancialMarketplace.Application.Errors;

public static partial class ApplicationErrors
{
    public static class AuthErrors
    {
        public static Error Unauthorized => Error.Unauthorized(
        code: "Auth.Unauthorized",
        description: "Falha na autenticação");
        public static Error BadRequest => Error.Validation(
        code: "Auth.BadRequest",
        description: "Email/senha inválidos");

        public static Error BadRefreshTokenRequest => Error.Validation(
        code: "Auth.BadTokenRequest",
        description: "Refresh token inválido");

        public static Error InvalidToken => Error.Validation(
        code: "Auth.InvalidToken",
        description: "Token inválido");

        public static Error ExpiredToken => Error.Validation(
        code: "Auth.ExpiredToken",
        description: "Token expirado");
    }
}
