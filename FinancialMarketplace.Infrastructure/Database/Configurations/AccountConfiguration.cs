using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialMarketplace.Infrastructure.Database.Configurations;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ConfigureAccountTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
