using FinancialMarketplace.Application;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Accounts;
using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Infrastructure.Database.Repositories;

public class UserRepository(MyDbContext dbContext) : IUserRepository
{
    private readonly MyDbContext _dbContext = dbContext;

    public async Task<User> CreateUser(User user, UserToken userToken, Account account)
    {
        _dbContext.Users.Add(user);

        _dbContext.UserTokens.Add(userToken);

        _dbContext.Accounts.Add(account);

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User[]> Get(GetUsersQueryOptions options)
    {
        var users = await _dbContext.Users.AsQueryable()
        .WhereIf(options.Name is not null, u => u.Name.Contains(options.Name!))
        .Where(u => u.DeletedAt == null)
        .ToArrayAsync();


        return users;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == email && u.DeletedAt == null);

        return user;
    }

    public async Task<User?> GetById(Guid id)
    {
        var user = await _dbContext.Users
        .Include(user => user.Role)
        .Include(user => user.Account)
        .Include(user => user.Account.Products)
        .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);

        return user;
    }

    public async Task CreatePassword(User user, string password)
    {
        user.Password = password;

        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        user.DeletedAt = DateTime.UtcNow;
        user.IsActive = false;

        await _dbContext.SaveChangesAsync();
    }
}
