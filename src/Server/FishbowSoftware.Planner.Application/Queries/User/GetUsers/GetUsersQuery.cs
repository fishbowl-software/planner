using FishbowSoftware.Planner.Shared;
using FishbowSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowSoftware.Planner.Application.Queries
{
    public class GetUsersQuery : PagedQuery, IRequest<PagedResult<UserDto>>
    {
    }
}
