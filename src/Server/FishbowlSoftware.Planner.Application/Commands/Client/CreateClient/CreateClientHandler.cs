using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class CreateClientHandler : RequestHandler<CreateClientCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public CreateClientHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result> HandleValidated(
        CreateClientCommand req, CancellationToken ct)
    {
        User? user = null;
        
        if (!string.IsNullOrEmpty(req.UserId))
        {
            user = await _uow.Repository<User>().GetAsync(i => i.Id == req.UserId);
        }
        
        var newClient = Client.Create(req.Name!, user);
        await _uow.Repository<Client>().AddAsync(newClient);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
