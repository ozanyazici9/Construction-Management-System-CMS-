using System.Linq.Dynamic.Core;

namespace WebApi.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return queryable;

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<T>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return queryable;

        return queryable.OrderBy(orderQuery);
    }
}
