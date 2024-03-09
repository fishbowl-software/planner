using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateApplicationHandler : RequestHandler<CreateApplicationCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public CreateApplicationHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(
        CreateApplicationCommand req, CancellationToken ct)
    {
        var project = await _uow.Repository<Project>().GetByIdAsync(req.ProjectId);
        
        if (project is null)
        {
            return Result.CreateFailure($"Project with the ID '{req.ProjectId}' does not exist");
        }
        
        var application = Domain.Entities.Application.Create(req.Name!, req.Description, project);
        
        await _uow.Repository<Domain.Entities.Application>().AddAsync(application);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
