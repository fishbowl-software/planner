using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetUsersValidator : AbstractValidator<GetClientsQuery>
{
    public GetUsersValidator()
    {
        RuleFor(i => i.Page).GreaterThanOrEqualTo(0);
        RuleFor(i => i.PageSize).GreaterThanOrEqualTo(1);
    }
}
