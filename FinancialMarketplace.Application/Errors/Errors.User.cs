using ErrorOr;

namespace FinancialMarketplace.Application.Errors;

public static partial class ApplicationErrors
{
    public static class User
    {
        public static Error BadRequest => Error.Validation(
        code: "User.BadRequest",
        description: "Email já cadastrado");

        public static Error Invalid => Error.Validation(
        code: "User.Invalid",
        description: "Usuário inválido");

        public static Error Permission => Error.Validation(
        code: "User.Permission",
        description: "Permissão para essa ação não concedida");

        public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "Usuário não encontrado");
    }
}
