using FinancialMarketplace.Api.Dtos.Users;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordRequestDto>
{

    public ResetPasswordDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
        .NotNull()
        .WithMessage("É necessário informar um e-mail")
        .EmailAddress()
        .WithMessage("O e-mail informado não é válido")
        .Must(x => x.Length > 0)
        .WithMessage("O e-mail informado não é válido");
    }
}
