using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateApplicationValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateApplicationValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
