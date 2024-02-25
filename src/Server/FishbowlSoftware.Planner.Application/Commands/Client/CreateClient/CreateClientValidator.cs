using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(i => i.AccountId).NotEmpty();
        RuleFor(i => i.FirstName).NotEmpty();
        RuleFor(i => i.LastName).NotEmpty();
        RuleFor(i => i.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
