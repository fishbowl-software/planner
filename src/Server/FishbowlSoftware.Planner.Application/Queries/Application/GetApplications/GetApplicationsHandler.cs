using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Domain.Specifications;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetApplicationsHandler : RequestHandler<GetApplicationsQuery, PagedResult<ApplicationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetApplicationsHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<PagedResult<ApplicationDto>> HandleValidated(
        GetApplicationsQuery req, CancellationToken ct)
    {
        var totalItems = await _uow.Repository<Domain.Entities.Application>().CountAsync();

        var applications = _uow.Repository<Domain.Entities.Application>()
            .ApplySpecification(new GetApplicationsPaged(req.Search, req.OrderBy, req.Page, req.PageSize))
            .Select(i => i.ToDto())
            .ToArray();

        return PagedResult<ApplicationDto>.CreateSuccess(applications, totalItems, req.PageSize);
    }
}
