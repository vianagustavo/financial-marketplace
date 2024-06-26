using FinancialMarketplace.Api.Dtos.Users;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
{

    public UpdateUserRequestDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("O e-mail informado não é válido");
    }
}
