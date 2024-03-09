using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateApplicationValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.ProjectId).NotEmpty();
    }
}
