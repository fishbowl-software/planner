using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteApplicationHandler : RequestHandler<DeleteApplicationCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public DeleteApplicationHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(
        DeleteApplicationCommand req, CancellationToken ct)
    {
        await _uow.Repository<Domain.Entities.Application>().DeleteByIdAsync(req.Id!);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
