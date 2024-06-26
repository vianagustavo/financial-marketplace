namespace FinancialMarketplace.Application.Contracts.External.Providers.Pdf;

[AttributeUsage(AttributeTargets.Property)]
public sealed class PdfTableColumnAttribute(string name, double width = 20) : Attribute
{
    public string Name { get; } = name;
    public double Width { get; } = width;
}
