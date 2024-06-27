using FinancialMarketplace.Domain.Transactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialMarketplace.Infrastructure.Database.Configurations;

public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        ConfigureTransactionTable(builder);
    }

    private static void ConfigureTransactionTable(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(q => q.Type)
            .HasConversion<string>();
    }
}
