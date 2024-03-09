using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Domain.Specifications;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetProjectsHandler : RequestHandler<GetProjectsQuery, PagedResult<ProjectDto>>
{
    private readonly IUnitOfWork _uow;

    public GetProjectsHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<PagedResult<ProjectDto>> HandleValidated(
        GetProjectsQuery req, CancellationToken ct)
    {
        var totalItems = await _uow.Repository<Project>().CountAsync();

        var projects = _uow.Repository<Project>()
            .ApplySpecification(new GetProjectsPaged(req.Search, req.OrderBy, req.Page, req.PageSize))
            .Select(i => i.ToDto())
            .ToArray();

        return PagedResult<ProjectDto>.CreateSuccess(projects, totalItems, req.PageSize);
    }
}
