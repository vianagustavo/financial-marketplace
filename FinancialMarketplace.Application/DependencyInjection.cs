using FinancialMarketplace.Application.Mapping;
using FinancialMarketplace.Application.Services;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Services.Workers;

using Microsoft.Extensions.DependencyInjection;

namespace FinancialMarketplace.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<INotifyExpiringProducts, NotifyExpiringProductsWorker>();

        services.AddMappings();

        return services;
    }
}
