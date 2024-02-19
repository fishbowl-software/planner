using System.Linq.Expressions;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.Entities;

namespace FishbowlSoftware.Planner.Domain.Specifications
{
    public class GetUsersPaged : BaseSpecification<User>
    {
        public GetUsersPaged(
            string? orderBy,
            int page,
            int pageSize)
        {
            ApplyOrderBy(orderBy);
            ApplyPaging(page, pageSize);
        }

        protected override Expression<Func<User, object?>> CreateOrderByExpression(string propertyName)
        {
            return propertyName switch
            {
                "phonenumber" => i => i.PhoneNumber,
                _ => i => i.Email
            };
        }
    }
}
