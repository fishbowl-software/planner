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
        var newClient = Client.Create(
            req.AccountId!,
            req.FirstName!,
            req.LastName!,
            req.Email!,
            req.PhoneNumber,
            req.Organization!);


        await _uow.Repository<Client>().AddAsync(newClient);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
}
