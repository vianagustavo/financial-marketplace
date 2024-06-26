using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialMarketplace.Api.Middlewares;
public class ErrorMiddleware(ILogger<ErrorMiddleware> logger) : IAsyncActionFilter
{
    private readonly ILogger<ErrorMiddleware> _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();
        var exception = result.Exception;
        result.Exception = null;

        if (exception != null)
        {
            _logger.LogError("Middleware Error: {Exception}", exception.ToString());

            result.Result = new ObjectResult(new { exception.Message })
            {
                StatusCode = 500,
            };

        }
    }
}
