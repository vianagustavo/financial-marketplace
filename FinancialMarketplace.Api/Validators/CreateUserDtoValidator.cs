using FinancialMarketplace.Api.Dtos.Users;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{

    public CreateUserDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("É necessário informar um nome")
            .Must(x => x.Length > 0)
            .WithMessage("Informe um nome válido");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("É necessário informar um e-mail")
            .EmailAddress()
            .WithMessage("O e-mail informado não é válido");
    }
}
