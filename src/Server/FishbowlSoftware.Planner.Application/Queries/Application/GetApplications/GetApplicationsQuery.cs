using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Queries;

public class GetApplicationsQuery : SearchableQuery, IRequest<PagedResult<ApplicationDto>>
{
}
