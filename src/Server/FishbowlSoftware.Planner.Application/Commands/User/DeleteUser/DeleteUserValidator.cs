using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(i => i.Id)
                .NotEmpty();
        }
    }
}
