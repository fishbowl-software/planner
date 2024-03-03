using FluentValidation;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
    }
}
