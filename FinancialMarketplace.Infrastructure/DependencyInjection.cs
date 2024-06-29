using Microsoft.Extensions.DependencyInjection;

using FinancialMarketplace.Application.Contracts.Database;
using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Contracts.Tools;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Infrastructure.Database;
using FinancialMarketplace.Infrastructure.Database.Repositories;
using FinancialMarketplace.Infrastructure.External;
using FinancialMarketplace.Infrastructure.External.TokenProvider;
using FinancialMarketplace.Infrastructure.Tools;
using System.Net.Mail;
using System.IdentityModel.Tokens.Jwt;

namespace FinancialMarketplace.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence()
            .AddProviders();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>();
        services.AddScoped<DapperConnection>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }

    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IEmailProvider, EmailProvider>();
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<ICryptoHandler, CryptoHandler>();
        services.AddScoped<IMemoryService, MemoryCacheService>();
        services.AddScoped<IEmailTemplateBuilder, EmailTemplateBuilder>();

        services.AddMemoryCache();
        services.AddScoped<SmtpClient>();
        services.AddScoped<JwtSecurityTokenHandler>();

        services.AddScoped<IEmailTemplateBuilder, EmailTemplateBuilder>();

        return services;
    }
}
