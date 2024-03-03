using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
    }
}
