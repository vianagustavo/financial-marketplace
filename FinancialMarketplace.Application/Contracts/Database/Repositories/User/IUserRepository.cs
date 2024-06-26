using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface IUserRepository
{
    Task<User> AddUserWithToken(User user, UserToken userToken);
    Task<User[]> Get(GetUsersQueryOptions options);
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(Guid id);
    Task CreatePassword(User user, string password);
    Task Update(User user);
    Task Delete(User user);
}
