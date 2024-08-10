using Application.Common;
using Domain.StableModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Extensions
{
    public static partial class Extension
    {
        async public static Task<IPagedResponse<T>> ToPaginateAsync<T>(this IQueryable<T> query, IPageable pageable, CancellationToken cancellation = default)
        where T : class
        {
            var count = await query.CountAsync(cancellation);
            var items = await query
                .Skip((pageable.Page - 1) * pageable.Size)
                .Take(pageable.Size)
                .ToListAsync(cancellation);

            return new PagedResponse<T>(pageable, items, count);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, ISortable sortable)
        where T : class
        {
            var column = string.IsNullOrWhiteSpace(sortable.Column) ? "Id" : sortable.Column;

            var property = typeof(T).GetProperty(column, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (property is  null)
                return query;

            var order = sortable.Order switch
            {
                SortOrders.Descending => "OrderByDescending",
                _ => "OrderBy"
            };

            var param = Expression.Parameter(typeof(T), "m");
            var propertyAccess = Expression.MakeMemberAccess(param, property);

            Expression expression = property.PropertyType == typeof(string)
                ? Expression.Call(propertyAccess, nameof(string.ToLower), Type.EmptyTypes)
                : propertyAccess;

            var orderByExpression = Expression.Lambda(expression, param);

            var resultExpression = Expression.Call(
                                            typeof(Queryable),
                                            order,
                                            new Type[] { typeof(T), property.PropertyType },
                                            query.Expression,
                                            Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
