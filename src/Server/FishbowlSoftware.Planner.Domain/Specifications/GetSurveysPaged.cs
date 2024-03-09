using System.Linq.Expressions;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.Entities;

namespace FishbowlSoftware.Planner.Domain.Specifications;

public class GetSurveysPaged : BaseSpecification<Survey>
{
    public GetSurveysPaged(
        string? searchQuery,
        string? orderBy,
        int page,
        int pageSize)
    {
        if (!string.IsNullOrEmpty(searchQuery))
        {
            Criteria = i =>
                i.Title != null && i.Title.Contains(searchQuery);
        }
        
        ApplyOrderBy(orderBy);
        ApplyPaging(page, pageSize);
    }

    protected override Expression<Func<Survey, object?>> CreateOrderByExpression(string propertyName)
    {
        return propertyName switch
        {
            "title" => i => i.Title,
            _ => i => i.Title
        };
    }
}
