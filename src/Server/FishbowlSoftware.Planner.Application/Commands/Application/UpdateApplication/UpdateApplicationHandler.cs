using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateApplicationHandler : RequestHandler<UpdateApplicationCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public UpdateApplicationHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(UpdateApplicationCommand req, CancellationToken ct)
    {
        var application = await _uow.Repository<Domain.Entities.Application>()
            .GetByIdAsync(req.Id!, false);
        
        if (application is null)
        {
            return Result.CreateFailure($"Project with the ID '{req.Id}' does not exist");
        }

        UpdateApplicationDetails(req, application);

        _uow.Repository<Domain.Entities.Application>().Update(application);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }

    private void UpdateApplicationDetails(UpdateApplicationCommand req, Domain.Entities.Application application)
    {
        if (!string.IsNullOrEmpty(req.Name) && req.Name != application.Name)
        {
            application.Name = req.Name;
        }

        if (req.Description != application.Description)
        {
            application.Description = req.Description;
        }
    }
}
