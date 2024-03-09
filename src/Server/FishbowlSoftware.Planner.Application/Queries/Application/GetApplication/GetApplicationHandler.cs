using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetApplicationHandler : RequestHandler<GetApplicationQuery, Result<ApplicationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetApplicationHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result<ApplicationDto>> HandleValidated(
        GetApplicationQuery req, CancellationToken ct)
    {
        var application = await _uow.Repository<Domain.Entities.Application>().GetByIdAsync(req.Id);
        
        if (application is null)
        {
            return Result<ApplicationDto>.CreateFailure($"Application with the ID '{req.Id}' does not exist");
        }
        
        var projectDto = application.ToDto();
        return Result<ApplicationDto>.CreateSuccess(projectDto);
    }
}
