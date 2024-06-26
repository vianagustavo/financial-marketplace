using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Common.Models;

namespace FinancialMarketplace.Domain.Users;

public class User : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Collection<UserToken> UserTokens { get; } = [];

    public User() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public User(
        Guid id,
        string name,
        string email,
        string password,
        bool isActive,
        DateTime updatedAt,
        DateTime deletedAt
    ) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        IsActive = isActive;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
}
