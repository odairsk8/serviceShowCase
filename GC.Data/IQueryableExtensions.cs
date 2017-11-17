using GC.Core.Interfaces;
using System.Linq;

namespace GC.Core.Querying
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject<T> queryObj)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !queryObj.OrderingMapping.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                query = query.OrderBy(queryObj.OrderingMapping[queryObj.SortBy]);
            else
                query = query.OrderByDescending(queryObj.OrderingMapping[queryObj.SortBy]);

            return query;
        }

        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, IQueryObject<T> queryObj)
        {
            var t = queryObj.FilteringMapping.Any(f => queryObj.FilterBy.Contains(f.Key));
            if (queryObj.FilterBy.Count() == 0 || !queryObj.FilteringMapping.Any(f => queryObj.FilterBy.Contains(f.Key)))
                return query;

            foreach (var item in queryObj.FilterBy)
                query = query.Where(queryObj.FilteringMapping[item]);

            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject<T> queryObject)
        {
            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10;

            if (queryObject.Page <= 0)
                queryObject.Page = 1;

            return query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}
