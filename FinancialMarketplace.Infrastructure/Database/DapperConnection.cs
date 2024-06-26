using FinancialMarketplace.Infrastructure.Database.DapperTypes.TypeHandlers;

using Dapper;

using Microsoft.Extensions.Logging;

using Npgsql;

namespace FinancialMarketplace.Infrastructure.Database;

public class DapperConnection : IDisposable
{
    private readonly string _connectionString;
    private readonly bool _dbLog;
    public NpgsqlConnection Connection { get; }

    public DapperConnection()
    {
        Config();

        string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        string dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "financial-marketplace";
        string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "26257";
        string dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "root";
        string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
        _dbLog = Environment.GetEnvironmentVariable("DB_LOG") == "1";

        _connectionString = $"Host={dbHost};Database={dbDatabase};Port={dbPort};Username={dbUsername};Password={dbPassword};Log Parameters=True;Include Error Detail=True;";

        if (_dbLog)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            NpgsqlLoggingConfiguration.InitializeLogging(loggerFactory);
        }

        Connection = new NpgsqlConnection(_connectionString);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Connection.Dispose();
        }
    }

    private static void Config()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
    }
}
