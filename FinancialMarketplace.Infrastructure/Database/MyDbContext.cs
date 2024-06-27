using Microsoft.EntityFrameworkCore;

using FinancialMarketplace.Domain.Users;
using FinancialMarketplace.Domain.Transactions;

namespace FinancialMarketplace.Infrastructure.Database;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    private readonly string _dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    private readonly string _dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "financial-marketplace";
    private readonly string _dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "26257";
    private readonly string _dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "root";
    private readonly string _dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
    private readonly bool _dbLog = Environment.GetEnvironmentVariable("DB_LOG") == "1";

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserToken> UserTokens { get; set; } = null!;
    public DbSet<Role> Role { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Host={_dbHost};Database={_dbDatabase};Port={_dbPort};Username={_dbUsername};Password={_dbPassword};";

        optionsBuilder.UseNpgsql(connectionString, options => options.EnableRetryOnFailure(6, TimeSpan.FromMilliseconds(300), ["40001"]))
            .UseSnakeCaseNamingConvention();

        if (_dbLog)
        {
            Console.WriteLine("CONNECTIONSTRING: " + connectionString);

            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information);

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
