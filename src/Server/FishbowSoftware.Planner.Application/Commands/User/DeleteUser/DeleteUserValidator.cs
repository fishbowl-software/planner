using FluentValidation;

namespace FishbowSoftware.Planner.Application.Commands
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
