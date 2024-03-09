using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Queries;

public class GetProjectQuery : IRequest<Result<ProjectDto>>
{
    public string? Id { get; set; }
}
