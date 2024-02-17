using FluentValidation;

namespace FishbowSoftware.Planner.Application.Queries
{
    internal class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
        }
    }
}
