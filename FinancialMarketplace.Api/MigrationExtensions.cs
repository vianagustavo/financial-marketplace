using FinancialMarketplace.Domain.Users;
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

        int userCount = dbContext.Users.CountAsync().GetAwaiter().GetResult();

        if (userCount == 0)
        {
            Console.WriteLine("[MIGRATION]: SEEDING ADMIN USER...");

            var user = new User()
            {
                Email = "admin@admin.com",
                IsActive = true,
                Name = "Admin",
                Password = "$2a$11$JF.6jOgcanRZ5tNAZSSHAOnl9scG8au5Q/PT3d6wpt7U4DQwqWWGO",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            Console.WriteLine("[MIGRATION]: ADMIN USER SEEDED.");
        }

    }
}
