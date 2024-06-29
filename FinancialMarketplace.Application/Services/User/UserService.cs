using System.Globalization;

using Microsoft.Extensions.Configuration;

using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Contracts.Tools;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Errors;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Users;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using Bogus;
using FinancialMarketplace.Domain.Accounts;

namespace FinancialMarketplace.Application.Services;

public class UserService(
    IConfiguration configuration,
    IAuthenticatedUserService authenticatedUserService,
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository,
    IEmailProvider emailProvider,
    ITokenProvider tokenProvider,
    ICryptoHandler cryptoHandler,
    IEmailTemplateBuilder emailTemplateBuilder
    ) : IUserService
{
    private readonly int _tokenSecondsExpiration = int.Parse(configuration["TOKEN_SECONDS_EXPIRATION"] ?? throw new ArgumentNullException("TOKEN_SECONDS_EXPIRATION"), NumberStyles.Integer, new CultureInfo("pt-BR"));
    private readonly string _clientRoleId = configuration["CLIENT_ROLE_ID"] ?? throw new ArgumentNullException("CLIENT_ROLE_ID");
    private readonly IAuthenticatedUserService _loggedUser = authenticatedUserService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserTokenRepository _userTokenRepository = userTokenRepository;
    private readonly IEmailProvider _emailProvider = emailProvider;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ICryptoHandler _cryptoHandler = cryptoHandler;
    private readonly IEmailTemplateBuilder _emailTemplateBuilder = emailTemplateBuilder;
    private readonly Faker _faker = new();

    public async Task<ErrorOr<User>> Add(CreateUserRequest createUser)
    {
        User? existingUser = await _userRepository.GetByEmail(createUser.Email);

        if (existingUser is not null)
            return ApplicationErrors.User.BadRequest;

        User user = new()
        {
            Name = createUser.Name,
            Email = createUser.Email,
            IsActive = true,
            RoleId = Guid.Parse(_clientRoleId)
        };

        UserToken userToken = new()
        {
            UserId = user.Id,
            Type = TokenType.CreatePassword,
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_tokenSecondsExpiration),
            CreatedAt = DateTime.UtcNow
        };

        Account account = new()
        {
            UserId = user.Id,
            Balance = 0,
            Number = _faker.Random.String2(2, "0123456789"),
            Branch = _faker.Random.String2(8, "0123456789"),
        };

        await _userRepository.CreateUser(user, userToken, account);

        TokenPayload payload = new()
        {
            UserId = user.Id.ToString(),
            TokenId = userToken.Id.ToString()
        };

        var token = _tokenProvider.Encode(payload, _tokenSecondsExpiration);

        await _emailProvider.Send(createUser.Email, "Sua conta FinancialMarketplace!", _emailTemplateBuilder.DefinePasswordTemplate(token, createUser.Name));

        return user;
    }

    public async Task<ErrorOr<User>> GetById(Guid id)
    {
        User? user = await _userRepository.GetById(id);

        if (user is null)
        {
            return ApplicationErrors.User.NotFound;
        }

        return user;
    }

    public async Task<ErrorOr<User[]>> Get(GetUsersQueryOptions options)
    {
        User[] user = await _userRepository.Get(options);

        return user;
    }
    public async Task<ErrorOr<bool>> CreatePassword(CreatePasswordRequest request)
    {
        var payload = _tokenProvider.Decode(request.Token);

        var userId = payload["userId"].ToString()!;
        var tokenId = payload["tokenId"].ToString()!;

        if (userId is null || tokenId is null)
            return ApplicationErrors.AuthErrors.InvalidToken;

        var user = await _userRepository.GetById(new Guid(userId));

        var userToken = await _userTokenRepository.GetUserActiveToken(new Guid(tokenId));

        if (userToken is null)
            return ApplicationErrors.AuthErrors.InvalidToken;

        await _userTokenRepository.UpdateUsedToken(userToken);

        var hashedPassword = _cryptoHandler.Encrypt(request.Password);

        await _userRepository.CreatePassword(user!, hashedPassword);

        return true;
    }

    public async Task<ErrorOr<bool>> ResetPassword(ResetPasswordRequest request)
    {
        User? user = await _userRepository.GetByEmail(request.Email);

        if (user is null)
            return ApplicationErrors.User.NotFound;

        await _userTokenRepository.UpdateUserActiveTokens(user.Id);

        UserToken userToken = new()
        {
            UserId = user.Id,
            Type = TokenType.ResetPassword,
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_tokenSecondsExpiration),
        };

        await _userTokenRepository.Add(userToken);

        TokenPayload payload = new()
        {
            UserId = user.Id.ToString(),
            TokenId = userToken.Id.ToString()
        };

        var token = _tokenProvider.Encode(payload, _tokenSecondsExpiration);

        await _emailProvider.Send(request.Email, "Link para alteração de senha", _emailTemplateBuilder.ResetPasswordTemplate(token, user.Name));

        return true;
    }

    public async Task<ErrorOr<bool>> UpdateRole(Guid id, UpdateUserRoleRequest request)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.ManageUsers))
        {
            return ApplicationErrors.User.Permission;
        }

        User? user = await _userRepository.GetById(id);

        if (user is null)
        {
            return ApplicationErrors.User.NotFound;
        }

        user.RoleId = request.RoleId;

        await _userRepository.Update(user);

        return true;
    }

    public async Task<ErrorOr<bool>> Delete(Guid id)
    {
        User? user = await _userRepository.GetById(id);

        if (user is null)
        {
            return ApplicationErrors.User.NotFound;
        }

        await _userRepository.Delete(user);

        return true;
    }

}
