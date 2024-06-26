using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCWalletManager.Infrastructure.Database.Configurations;

public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        ConfigureUserTokenTable(builder);
    }

    private static void ConfigureUserTokenTable(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("user_tokens");

        builder.Property(q => q.Type)
            .HasConversion<string>();
    }
}