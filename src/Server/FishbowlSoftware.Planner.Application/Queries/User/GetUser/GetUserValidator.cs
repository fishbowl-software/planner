using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Queries
{
    internal class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
        }
    }
}
