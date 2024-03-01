using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteUserValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty();
    }
}
