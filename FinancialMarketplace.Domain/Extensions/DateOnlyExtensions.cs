namespace FinancialMarketplace.Domain;

public static class DateOnlyExtensions
{
    public static int MonthsBetween(this DateOnly start, DateOnly end)
    {
        int months = (end.Year - start.Year) * 12 + end.Month - start.Month;

        return months < 0 ? 0 : months;
    }

    public static bool SameMonthOrEarlierThan(this DateOnly x, DateOnly y)
    {
        return x.Year < y.Year || (x.Year == y.Year && x.Month <= y.Month);
    }
    public static bool SameMonthLaterThan(this DateOnly x, DateOnly y)
    {
        return x.Year > y.Year || (x.Year == y.Year && x.Month >= y.Month);
    }

    public static void SetFirstDayOfMonth(this DateOnly date)
    {
        date.AddDays(-(date.Day - 1));
    }
}
