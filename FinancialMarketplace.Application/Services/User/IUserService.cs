using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Users;

using ErrorOr;

namespace FinancialMarketplace.Application.Services;

public interface IUserService
{
    public Task<ErrorOr<User>> Add(CreateUserRequest createUser);
    Task<ErrorOr<User>> GetById(Guid id);
    Task<ErrorOr<User[]>> Get(GetUsersQueryOptions options);
    Task<ErrorOr<bool>> CreatePassword(CreatePasswordRequest request);
    Task<ErrorOr<bool>> ResetPassword(ResetPasswordRequest request);
    Task<ErrorOr<bool>> Update(Guid id, UpdateUserRequest request);
    Task<ErrorOr<bool>> Delete(Guid id);
}
