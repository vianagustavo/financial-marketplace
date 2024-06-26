namespace FinancialMarketplace.Application.Contracts.External.Providers.Sheet;

public interface ISheetProvider
{
    ReadSheetResponse<T>[] ReadCsv<T>(MemoryStream stream);
    ReadSheetResponse<T>[] ReadXlsx<T>(MemoryStream stream);
    Task<MemoryStream> WriteXlsx<T>(string sheetName, T[] data);
}
