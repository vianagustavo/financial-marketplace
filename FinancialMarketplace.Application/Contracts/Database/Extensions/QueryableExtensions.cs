using System.Linq.Expressions;

using BCWalletManager.Application.Contracts.Common.Enums;

namespace FinancialMarketplace.Application;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    public static IQueryable<T> OrderBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector, SortDirection sortDirection)
    {
        return sortDirection == SortDirection.Ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
    }

    public static bool IsNullOrEmpty(this Array? array)
    {
        return array is null || array.Length == 0;
    }

    public static bool IsNullOrEmpty(this string? str)
    {
        return string.IsNullOrEmpty(str);
    }
}
