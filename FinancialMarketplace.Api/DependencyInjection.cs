using System.Reflection;
using System.Text.Json.Serialization;

using FinancialMarketplace.Api.Errors;
using FinancialMarketplace.Api.Mapping;
using FinancialMarketplace.Api.Middlewares;
using FinancialMarketplace.Api.Swagger;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialMarketplace.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(mvcOptions => mvcOptions.Filters.AddService<ErrorMiddleware>())
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddTransient<ErrorMiddleware>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

        services.AddMappings();

        return services;
    }
}
