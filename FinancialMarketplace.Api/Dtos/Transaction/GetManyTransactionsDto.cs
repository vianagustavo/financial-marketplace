using FinancialMarketplace.Api.Dtos.Transactions;
using FinancialMarketplace.Application.Contracts.Services.Common;

namespace FinancialMarketplace.Api.Dtos.Product;

public record GetManyTransactionsResponseDto : PaginatedResponse<BasicTransactionDto> { }
