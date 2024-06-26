using FinancialMarketplace.Api.Dtos.Users;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class CreatePasswordDtoValidator : AbstractValidator<CreatePasswordRequestDto>
{

    public CreatePasswordDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Token)
            .NotNull()
            .WithMessage("É necessário informar um token")
            .Must(x => x.Length > 0)
            .WithMessage("Informe um token válido");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("É necessário informar uma senha")
            .Must(x => x.Length > 0)
            .WithMessage("A senha informada não é válido");
    }
}
