using System.Globalization;
namespace FinancialMarketplace.Application.Tools;

public static class Formatter
{
    public static string MoneyString(long value)
    {
        return "R$ " + ((decimal)value / 100).ToString("0.00", CultureInfo.InvariantCulture);
    }

    public static string DateTimeString(DateTime? date, bool appliesTimezone = true)
    {
        if (!date.HasValue)
        {
            return "";
        }

        if (appliesTimezone)
        {
            date = date.Value.AddHours(-3);
        }

        return date.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
