using System.Linq.Expressions;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.Entities;

namespace FishbowlSoftware.Planner.Domain.Specifications;

public class GetClientsPaged : BaseSpecification<Client>
{
    public GetClientsPaged(
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

    protected override Expression<Func<Client, object?>> CreateOrderByExpression(string propertyName)
    {
        return propertyName switch
        {
            "name" => i => i.Name,
            _ => i => i.Name
        };
    }
}
