using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Common.Models;

namespace FinancialMarketplace.Domain.Users;

public class Product : Entity
{
    public required string Name { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public required bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Collection<Account> Accounts { get; } = [];

    public Product() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Product(
        Guid id,
        string name,
        string email,
        string password,
        bool isActive,
        DateTime updatedAt,
        DateTime deletedAt,
        Guid roleId,
        Role role,
        Account account
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
