using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCWalletManager.Infrastructure.Database.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        ConfigureRoleTable(builder);
    }

    private static void ConfigureRoleTable(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.Property(r => r.Permissions)
            .HasConversion<string>();
    }
}