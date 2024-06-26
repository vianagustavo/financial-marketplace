using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FinancialMarketplace.Api.Dtos.Users;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Services;

using MapsterMapper;

namespace FinancialMarketplace.Api.Controllers;

[Route("users")]
public class UserController(IUserService userService, IMapper mapper) : ApiController
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicUserDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(CreateUserDto createUserDto)
    {
        var user = await _userService.Add(createUserDto);

        return user.Match(
            result => Created("", _mapper.Map<BasicUserDto>(result)),
            Problem
        );
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicUserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetById(id);

        return user.Match(u => Ok(_mapper.Map<BasicUserDto>(u)), Problem);
    }

    [HttpGet()]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicUserDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(GetUsersQueryOptionsDto optionsDto)
    {
        var options = _mapper.Map<GetUsersQueryOptions>(optionsDto);
        var users = await _userService.Get(options);

        return users.Match(u => Ok(_mapper.Map<BasicUserDto[]>(u)), Problem);
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicUserDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Patch(Guid id, UpdateUserRequestDto request)
    {
        var user = await _userService.Update(id, request);

        return user.Match(u => Ok(_mapper.Map<bool>(u)), Problem);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicUserDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userService.Delete(id);

        return user.Match(u => Ok(_mapper.Map<bool>(u)), Problem);
    }

    [HttpPatch("password")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePassword(CreatePasswordRequestDto request)
    {
        var response = await _userService.CreatePassword(request);

        return response.Match(r => Ok(_mapper.Map<bool>(r)), Problem);
    }

    [HttpPost("reset-password")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
    {
        var response = await _userService.ResetPassword(request);

        return response.Match(r => Ok(_mapper.Map<bool>(r)), Problem);
    }
}
