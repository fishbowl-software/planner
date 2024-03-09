using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetApplicationValidator : AbstractValidator<GetApplicationQuery>
{
    public GetApplicationValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
