using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
