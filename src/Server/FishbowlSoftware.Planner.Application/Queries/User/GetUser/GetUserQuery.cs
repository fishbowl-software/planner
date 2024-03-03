using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Queries;

public class GetUserQuery : IRequest<Result<UserDto>>
{
    public required string Id { get; set; }
}
