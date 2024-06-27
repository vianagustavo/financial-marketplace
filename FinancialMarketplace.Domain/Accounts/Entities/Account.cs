using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Common.Models;

namespace FinancialMarketplace.Domain.Users;

public class Account : Entity
{
    public required string Number { get; set; } = null!;
    public required string Branch { get; set; } = null!;
    public required decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Collection<Product> Products { get; } = [];

    public Account() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Account(
        Guid id,
        string number,
        string branch,
        decimal balance,
        DateTime updatedAt,
        DateTime deletedAt,
        Guid userId,
        User user
    ) : base(id)
    {
        Number = number;
        Branch = branch;
        Balance = balance;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
        UserId = userId;
        User = user;
    }
}
