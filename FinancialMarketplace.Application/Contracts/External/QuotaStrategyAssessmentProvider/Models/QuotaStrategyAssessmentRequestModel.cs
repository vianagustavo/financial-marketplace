using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FinancialMarketplace.Application.Contracts.External.QuotaStrategyAssessmentProvider.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum QuotaStrategyAssessmentCashFlowType
{
    [EnumMember(Value = "CREDIT")]
    Credit = 0,
    [EnumMember(Value = "DEBIT")]
    Debit,
}

[JsonConverter(typeof(StringEnumConverter))]
public enum QuotaStrategyAssessmentCashFlowPaymentType
{
    [EnumMember(Value = "MENSALIDADE")]
    Installment,
    [EnumMember(Value = "LANCE")]
    SelfResourcesBid,
    [EnumMember(Value = "LIQUIDACAO")]
    Liquidation,
    [EnumMember(Value = "COMISSAO_BC_2")]
    BomConsorcioCommission2,
    [EnumMember(Value = "TAXA_CESSAO_ADM")]
    AdministratorTax,
    [EnumMember(Value = "TAXA_CESSAO_BC")]
    BomConsorcioTax,
    [EnumMember(Value = "COMISSAO_ADM")]
    AdministratorCommission,
    [EnumMember(Value = "COMISSAO_BC")]
    BomConsorcioCommission,
    [EnumMember(Value = "INADIMPLENCIA")]
    Debt,
    [EnumMember(Value = "COTISTA")]
    QuotaOwnerPayment,
}

public record QuotaStrategyAssessmentCashFlow
{
    [JsonProperty("eventDate")]
    public required DateOnly Date { get; set; }

    [JsonProperty("type")]
    public required QuotaStrategyAssessmentCashFlowType Type { get; set; }

    [JsonProperty("paymentType")]
    public required QuotaStrategyAssessmentCashFlowPaymentType PaymentType { get; set; }

    [JsonProperty("value")]
    public required decimal Value { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum QuotaAssessmentType
{
    [EnumMember(Value = "ACTIVE")]
    Active,
    [EnumMember(Value = "INACTIVE")]
    Inactive,
}

[JsonConverter(typeof(StringEnumConverter))]
public enum QuotaStrategyType
{
    [EnumMember(Value = "CARRY")]
    Carry = 0,
    [EnumMember(Value = "CONTEMPLATE")]
    Contemplate,
}

public enum AssetType
{
    [EnumMember(Value = "APPLIANCE")]
    Appliance,
    [EnumMember(Value = "GAME")]
    Game,
    [EnumMember(Value = "HOME")]
    Home,
    [EnumMember(Value = "MOTORCYCLE")]
    Motorcycle,
    [EnumMember(Value = "REFORM")]
    Reform,
    [EnumMember(Value = "SERVICE")]
    Service,
    [EnumMember(Value = "SOLAR")]
    Solar,
    [EnumMember(Value = "TRACTOR")]
    Tractor,
    [EnumMember(Value = "VEHICLE")]
    Vehicle,
}


public record QuotaStrategyAssessment
{
    [JsonProperty("referenceId")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [JsonProperty("consortiumCompanyId")]
    public required int ConsortiumCompanyId { get; set; }

    [JsonProperty("product")]
    public required AssetType Asset { get; set; }

    [JsonProperty("quotaType")]
    public required QuotaAssessmentType Type { get; set; }

    [JsonProperty("quotaValue")]
    public required decimal Value { get; set; }

    [JsonProperty("percentPaid")]
    public required decimal PercentagePaid { get; set; }

    [JsonProperty("paidCommonFund")]
    public required decimal CommonFundPaid { get; set; }

    [JsonProperty("debitTotalValue")]
    public required decimal LateValue { get; set; }

    [JsonProperty("lastAssemblyDate")]
    public required DateOnly LastMeetingAt { get; set; }

    [JsonProperty("installmentsToPay")]
    public required int InstallmentsRemaining { get; set; }

    [JsonProperty("bCCommissionAmount")]
    public required decimal BCCommissionAmount { get; set; }

    [JsonProperty("consortiumCompanyCommissionAmount")]
    public required decimal ConsortiumCompanyCommissionAmount { get; set; }

    [JsonProperty("consortiumCompanyTransferAmount")]
    public required decimal ConsortiumCompanyTransferAmount { get; set; }

    [JsonProperty("bCTransferAmount")]
    public required decimal BCTransferAmount { get; set; }

    [JsonProperty("bCCommission2")]
    public required decimal BCCommission2 { get; set; }

    [JsonProperty("fundId")]
    public required string FundId { get; set; }

    [JsonProperty("currentCashFlow")]
    public required QuotaStrategyAssessmentCashFlow[] MoneyTransactions { get; set; }

    [JsonProperty("appointedStrategy")]
    public required QuotaStrategyType AppointedStrategy { get; set; }
}

public record CreateQuotaStrategyAssessmentRequest
{
    [JsonProperty("operationId")]
    public required Guid BatchId { get; init; }

    [JsonProperty("quotas")]
    public required QuotaStrategyAssessment[] Quotas { get; init; }
}

public record CreateQuotaStrategyAssessmentResponse
{
    [JsonProperty("operationId")]
    public required Guid OperationId { get; init; }
}
