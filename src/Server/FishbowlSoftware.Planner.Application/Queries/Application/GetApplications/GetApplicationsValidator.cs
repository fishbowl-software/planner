using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetApplicationsValidator : AbstractValidator<GetApplicationsQuery>
{
    public GetApplicationsValidator()
    {
    }
}
