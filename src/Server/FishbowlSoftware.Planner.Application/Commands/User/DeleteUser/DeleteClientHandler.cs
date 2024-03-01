using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class DeleteUserHandler : RequestHandler<DeleteClientCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public DeleteUserHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result> HandleValidated(
        DeleteClientCommand req, CancellationToken ct)
    {
        var user = await _uow.Repository<User>()
            .GetAsync(i => i.Id == req.Id);

        if (user is null)
        {
            return Result.CreateError($"Could not find a user with ID {req.Id}");
        }

        _uow.Repository<User>().Delete(user);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
