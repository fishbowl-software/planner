using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetProjectHandler : RequestHandler<GetProjectQuery, Result<ProjectDto>>
{
    private readonly IUnitOfWork _uow;

    public GetProjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result<ProjectDto>> HandleValidated(
        GetProjectQuery req, CancellationToken ct)
    {
        var project = await _uow.Repository<Project>().GetByIdAsync(req.Id);
        
        if (project is null)
        {
            return Result<ProjectDto>.CreateFailure($"Project with the ID '{req.Id}' does not exist");
        }
        
        var projectDto = project.ToDto();
        return Result<ProjectDto>.CreateSuccess(projectDto);
    }
}
