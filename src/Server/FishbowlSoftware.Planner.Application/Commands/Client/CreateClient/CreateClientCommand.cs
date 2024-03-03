using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class CreateClientCommand : IRequest<Result>
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
}
