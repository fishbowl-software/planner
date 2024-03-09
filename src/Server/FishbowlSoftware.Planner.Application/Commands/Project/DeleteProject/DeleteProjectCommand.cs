using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class DeleteProjectCommand : IRequest<Result>
{
    public string? Id { get; set; }
}
