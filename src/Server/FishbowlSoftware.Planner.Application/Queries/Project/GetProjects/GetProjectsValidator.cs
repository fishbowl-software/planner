using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetProjectsValidator : AbstractValidator<GetProjectsQuery>
{
    public GetProjectsValidator()
    {
    }
}
