using FinancialMarketplace.Application.Contracts.Services.Common;

namespace FinancialMarketplace.Api.Dtos.Product;

public record GetManyProductsResponseDto : PaginatedResponse<BasicProductDto> { }
