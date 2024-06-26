using FinancialMarketplace.Api.Dtos.Users;

using FluentValidation;

namespace FinancialMarketplace.Api.Validators;

public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRoleDto>
{

    public UpdateUserRequestDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RoleId)
            .NotNull()
            .WithMessage("RoleId deve ser informado");
    }
}
