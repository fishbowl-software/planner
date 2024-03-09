using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.ClientId).NotEmpty();
    }
}
