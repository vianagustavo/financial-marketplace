using FinancialMarketplace.Application.Contracts.Services.Common;

namespace FinancialMarketplace.Api.Dtos.Common;

public record PaginatedResponseDto<T> : PaginatedResponse<T> { }
