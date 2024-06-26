using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Services.Auth;

namespace FinancialMarketplace.Api.Middlewares;
public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var id = context.User.FindFirst("userId")!.Value;

            context.Response.ContentType = "application/json";

            if (Guid.TryParse(id, out var userId))
            {
                authenticatedUserService.User = await userRepository.GetById(userId);
            }
            else
            {
                context.Response.StatusCode = 400;
                return;
            }

            if (authenticatedUserService.User is null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            if (authenticatedUserService.User.IsActive is false)
            {
                context.Response.StatusCode = 401;
                return;
            }
        }

        await _next.Invoke(context);
    }
}
