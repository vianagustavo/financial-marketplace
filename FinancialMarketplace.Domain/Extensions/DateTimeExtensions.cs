using System.Globalization;

namespace FinancialMarketplace.Domain;

public static class DateTimeExtensions
{
    public static DateTime? SetKindToUtc(this DateTime? value)
    {
        if (value is null)
            return value;

        return value.Value.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc).AddHours(3)
            : value.Value.ToUniversalTime();
    }

    public static DateTime ParseDateOnlyToDateTimeUtc(this DateOnly value)
    {
        return DateTime.SpecifyKind(value.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
    }

    public static DateTime ParseMonthYear(this string monthYear)
    {
        return DateTime.ParseExact(monthYear, "MM/yy", CultureInfo.InvariantCulture);
    }

    public static DateOnly? ParseToDateOnly(this DateTime? date)
    {
        return date is not null ? DateOnly.FromDateTime(((DateTime)date).AddHours(-3)) : null;
    }
}
