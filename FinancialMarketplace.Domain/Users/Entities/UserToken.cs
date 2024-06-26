using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Domain.Users;

public class UserToken : Entity
{
    public UserToken() : base(Guid.NewGuid()) { }
    public Guid UserId { get; set; }
    public TokenType Type { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

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
