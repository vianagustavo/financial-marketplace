using FinancialMarketplace.Application.Contracts.External.QuotaStrategyAssessmentProvider.Models;

using Refit;

namespace FinancialMarketplace.Application.Contracts.External.QuotaStrategyAssessmentProvider;

public interface IQuotaStrategyAssessmentProvider
{
    [Post("/WalletManagement/StrategyAnalisys")]
    Task<CreateQuotaStrategyAssessmentResponse> CreateStrategyAssessment(CreateQuotaStrategyAssessmentRequest request);
}
