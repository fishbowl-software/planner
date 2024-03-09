using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetProjectValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
