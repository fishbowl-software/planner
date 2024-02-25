using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientHandler : RequestHandler<GetClientQuery, Result<ClientDto>>
{
    private readonly IUnitOfWork _uow;

    public GetClientHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result<ClientDto>> HandleValidated(
        GetClientQuery req, CancellationToken ct)
    {
        var clientEntity = await _uow.Repository<Client>()
            .GetAsync(i => i.Id == req.Id || i.AccountId == req.Id || i.Email == req.Id);

        if (clientEntity is null)
        {
            return Result<ClientDto>.CreateError($"Could not find a client with ID {req.Id}");
        }

        var clientDto = clientEntity.ToDto();
        return Result<ClientDto>.CreateSuccess(clientDto);
    }
}
