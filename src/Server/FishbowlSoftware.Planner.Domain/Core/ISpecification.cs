﻿using System.Linq.Expressions;

namespace FishbowlSoftware.Planner.Domain.Core
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        Expression<Func<T, object?>>? OrderBy { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        List<FilterExpression> FilterExpressions { get; }
        int PageSize { get; }
        int Page { get; }
        bool IsPagingEnabled { get; }
        bool IsDescending { get; }
    }
}
