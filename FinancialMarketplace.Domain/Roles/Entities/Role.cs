using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Domain.Users;

public class Role : Entity
{
    public required string Name { get; set; } = null!;
    public required Permissions Permissions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Collection<User> Users { get; } = [];

    public Role() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }

    public Role(
        Guid id,
        string name,
        Permissions permissions,
        DateTime updatedAt
    ) : base(id)
    {
        Name = name;
        Permissions = permissions;
        UpdatedAt = updatedAt;
    }
}
