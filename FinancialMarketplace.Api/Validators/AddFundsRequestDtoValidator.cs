using FinancialMarketplace.Api.Dtos.Account;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class AddFundsDtoValidator : AbstractValidator<AddFundsRequestDto>
{

    public AddFundsDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("É necessário informar um valor")
            .Must(x => x > 0)
            .WithMessage("O valor a ser adicionado não pode ser igual ou menor que 0");
    }
}
