using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(i => i.Id)
                .NotEmpty();
        }
    }
}
