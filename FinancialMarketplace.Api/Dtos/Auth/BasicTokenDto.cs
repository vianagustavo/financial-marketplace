namespace FinancialMarketplace.Api.Dtos.Auth;

public record BasicTokenDto
{
    public string Token { get; set; } = "";
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = "";
}