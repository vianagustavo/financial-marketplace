using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialMarketplace.Infrastructure.Database.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        ConfigureRoleTable(builder);
    }

    private static void ConfigureRoleTable(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.Permissions)
            .HasConversion(
            p => string.Join(',', p),
            p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Enum.Parse<UserPermissions>).ToArray()
        );
    }
}
