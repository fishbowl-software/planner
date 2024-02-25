using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientsValidator : AbstractValidator<GetClientsQuery>
{
    public GetClientsValidator()
    {
        RuleFor(i => i.Page)
            .GreaterThanOrEqualTo(0);

        RuleFor(i => i.PageSize)
            .GreaterThanOrEqualTo(1);
    }
}
