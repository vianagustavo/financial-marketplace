namespace FinancialMarketplace.Application.Contracts.External.Providers.Sheet;

[AttributeUsage(AttributeTargets.Property)]
public sealed class SheetColumnAttribute(string name, int width = 20) : Attribute
{
    public string Name { get; } = name;
    public int Width { get; } = width;
}
