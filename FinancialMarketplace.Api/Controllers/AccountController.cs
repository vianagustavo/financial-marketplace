using FinancialMarketplace.Api.Dtos.Account;
using FinancialMarketplace.Application.Services;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Controllers;

[Route("accounts")]
public class AccountController(IAccountService accountService, IMapper mapper) : ApiController
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    [HttpPost("funds")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddFundsRequestDto addFundsRequestDto)
    {
        var funds = await _accountService.AddFunds(addFundsRequestDto);

        return funds.Match(
            result => Ok(_mapper.Map<bool>(result)),
            Problem
        );
    }

    [HttpPost("product/{productId:guid}/buy")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProduct(Guid productId)
    {
        var response = await _accountService.AddProduct(productId);

        return response.Match(
            result => Ok(_mapper.Map<bool>(result)),
            Problem
        );
    }

    [HttpPost("product/{productId:guid}/sell")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SellProduct(Guid productId)
    {
        var response = await _accountService.SellProduct(productId);

        return response.Match(
            result => Ok(_mapper.Map<bool>(result)),
            Problem
        );
    }
}
