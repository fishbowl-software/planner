using FishbowSoftware.Planner.Shared;
using FishbowSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowSoftware.Planner.Application.Queries
{
    public class GetUserQuery : IRequest<Result<UserDto>>
    {
        public required string Id { get; set; }
    }
}
