using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateProjectHandler : RequestHandler<CreateProjectCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public CreateProjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(
        CreateProjectCommand req, CancellationToken ct)
    {
        var client = await _uow.Repository<Client>().GetByIdAsync(req.ClientId);
        
        if (client is null)
        {
            return Result.CreateFailure($"Client with the ID '{req.ClientId}' does not exist");
        }
        
        var project = Project.Create(req.Name!, req.Description, client);
        await _uow.Repository<Project>().AddAsync(project);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
