using System.Reflection;

using Mapster;

using MapsterMapper;

namespace FinancialMarketplace.Api.Mapping;

public static class ApiMappingDependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}