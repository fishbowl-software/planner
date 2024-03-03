using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class UpdateClientCommand : IRequest<Result>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }
}
