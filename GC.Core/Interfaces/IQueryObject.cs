using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GC.Core.Interfaces
{
    public interface IQueryObject<T>
    {
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
        string SortBy { get; set; }

        Dictionary<string, Expression<Func<T, object>>> OrderingMapping { get; set; }
        List<Expression<Func<T, bool>>> GetConditions();
    }
}
