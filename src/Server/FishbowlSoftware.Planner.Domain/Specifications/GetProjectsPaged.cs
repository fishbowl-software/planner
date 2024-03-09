using System.Linq.Expressions;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.Entities;

namespace FishbowlSoftware.Planner.Domain.Specifications;

public class GetProjectsPaged : BaseSpecification<Project>
{
    public GetProjectsPaged(
        string? searchQuery,
        string? orderBy,
        int page,
        int pageSize)
    {
        if (!string.IsNullOrEmpty(searchQuery))
        {
            Criteria = i =>
                i.Name != null && i.Name.Contains(searchQuery);
        }
        
        ApplyOrderBy(orderBy);
        ApplyPaging(page, pageSize);
    }

    protected override Expression<Func<Project, object?>> CreateOrderByExpression(string propertyName)
    {
        return propertyName switch
        {
            "name" => i => i.Name,
            "description" => i => i.Description,
            "client.name" => i => i.Client!.Name,
            _ => i => i.Name
        };
    }
}
