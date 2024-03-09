using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class CreateProjectCommand : IRequest<Result>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ClientId { get; set; }
}
