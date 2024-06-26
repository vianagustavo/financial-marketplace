using FinancialMarketplace.Domain.Exceptions;

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


            if (exception is DomainInvalidArgumentException)
            {
                result.Result = new ObjectResult(new { Code = 400, exception.Message })
                {
                    StatusCode = 400,
                };
            }
            else if (exception is DomainNotFoundException)
            {
                result.Result = new ObjectResult(new { Code = 404, exception.Message })
                {
                    StatusCode = 404,
                };
            }
            else if (exception is DomainConflictException)
            {
                result.Result = new ObjectResult(new { Code = 409, exception.Message })
                {
                    StatusCode = 409,
                };
            }
            else
            {
                result.Result = new ObjectResult(new { exception.Message })
                {
                    StatusCode = 500,
                };
            }
        }
    }
}
