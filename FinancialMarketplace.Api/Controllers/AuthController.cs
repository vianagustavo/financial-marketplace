using FinancialMarketplace.Api.Dtos.Auth;
using FinancialMarketplace.Application.Services;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Controllers;

[Route("auth")]
public class AuthController(IAuthService authService, IMapper mapper) : ApiController
{
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;


    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType<BasicTokenDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(LoginRequestDto request)
    {
        var token = await _authService.Login(request);

        return token.Match(t => Ok(_mapper.Map<BasicTokenDto>(t)), Problem);
    }

    [HttpPost("refresh")]
    [Produces("application/json")]
    [ProducesResponseType<BasicTokenDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
    {
        var token = await _authService.Refresh(request);

        return token.Match(t => Ok(_mapper.Map<BasicTokenDto>(t)), Problem);
    }
}
