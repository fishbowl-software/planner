using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
