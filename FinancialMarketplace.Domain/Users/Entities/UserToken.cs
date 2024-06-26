using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Domain.Users;

public class UserToken : Entity
{
    public required Guid UserId { get; set; }
    public required TokenType Type { get; set; }
    public required bool IsUsed { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserToken() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public UserToken(
        Guid id,
        Guid userId,
        TokenType type,
        bool isUsed,
        DateTime expiresAt,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        UserId = userId;
        Type = type;
        IsUsed = isUsed;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
