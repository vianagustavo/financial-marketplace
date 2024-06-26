using FinancialMarketplace.Api.Dtos.Auth;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{

    public LoginRequestDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("É necessário informar uma senha")
            .Must(x => x.Length > 0)
            .WithMessage("Informe uma senha válida");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("É necessário informar um e-mail")
            .EmailAddress()
            .WithMessage("O e-mail informado não é válido")
            .Must(x => x.Length > 0)
            .WithMessage("O e-mail informado não é válido");
    }
}
