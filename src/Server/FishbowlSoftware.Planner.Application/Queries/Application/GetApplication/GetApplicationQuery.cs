using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Queries;

public class GetApplicationQuery : IRequest<Result<ApplicationDto>>
{
    public string? Id { get; set; }
}
