using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteProjectHandler : RequestHandler<DeleteProjectCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public DeleteProjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(
        DeleteProjectCommand req, CancellationToken ct)
    {
        await _uow.Repository<Project>().DeleteByIdAsync(req.Id!);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
