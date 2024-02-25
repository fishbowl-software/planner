using System.Linq.Expressions;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.Entities;

namespace FishbowlSoftware.Planner.Domain.Specifications;

public class GetClientsPaged : BaseSpecification<Client>
{
    public GetClientsPaged(
        string? orderBy,
        int page,
        int pageSize)
    {
        ApplyOrderBy(orderBy);
        ApplyPaging(page, pageSize);
    }

    protected override Expression<Func<Client, object?>> CreateOrderByExpression(string propertyName)
    {
        return propertyName switch
        {
            "phonenumber" => i => i.PhoneNumber,
            "organization" => i => i.Organization,
            "address" => i => i.Address.Line1,
            "firstname" => i => i.FirstName,
            "lastname" => i => i.LastName,
            _ => i => i.Email
        };
    }
}
