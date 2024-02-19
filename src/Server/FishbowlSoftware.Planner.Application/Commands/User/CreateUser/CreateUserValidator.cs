using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(i => i.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
