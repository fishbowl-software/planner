using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteApplicationValidator : AbstractValidator<DeleteApplicationCommand>
{
    public DeleteApplicationValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
