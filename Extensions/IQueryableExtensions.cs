using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using dncNgCarSales.Core.Models;

namespace dncNgCarSales.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(
            this IQueryable<T> query, 
            IQueryObject queryObj, 
            Dictionary<string, Expression<Func<T, object>>> columnsMap
        )
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || 
                !columnsMap.ContainsKey(queryObj.SortBy))
            {
                return query;
            }

            if (queryObj.IsSortAscending)
            {
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            }
            else
            {
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
        }   

        public static IQueryable<T> ApplyPaging<T>(
            this IQueryable<T> query,
            IQueryObject queryObj
        )
        {
            queryObj.Page = (queryObj.Page <= 0) ? 1 : queryObj.PageSize;
            queryObj.PageSize = (queryObj.PageSize <= 0) ? 10 : queryObj.PageSize;
            
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}