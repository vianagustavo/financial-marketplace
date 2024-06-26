using System.Reflection;

using Mapster;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

namespace FinancialMarketplace.Application.Mapping;

public static class MappingDependencyInjection
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