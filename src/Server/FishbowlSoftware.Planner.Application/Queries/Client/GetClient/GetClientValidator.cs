using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientValidator : AbstractValidator<GetClientQuery>
{
    public GetClientValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
