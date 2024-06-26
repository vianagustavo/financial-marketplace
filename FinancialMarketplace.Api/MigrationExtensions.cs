using FinancialMarketplace.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Api;
public static class MigrationExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        Console.WriteLine("[MIGRATION]: MIGRATING DATABASE...");

        dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

        Console.WriteLine("[MIGRATION]: DATABASE MIGRATED.");
    }
}
