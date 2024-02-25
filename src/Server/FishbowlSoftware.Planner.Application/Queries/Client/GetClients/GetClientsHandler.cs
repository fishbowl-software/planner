using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Domain.Specifications;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientsHandler : RequestHandler<GetClientsQuery, PagedResult<ClientDto>>
{
    private readonly IUnitOfWork _uow;

    public GetClientsHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<PagedResult<ClientDto>> HandleValidated(
        GetClientsQuery req, CancellationToken ct)
    {
        var totalItems = await _uow.Repository<Client>().CountAsync();

        var users = _uow.Repository<Client>()
            .ApplySpecification(new GetClientsPaged(req.OrderBy, req.Page, req.PageSize))
            .Select(i => i.ToDto())
            .ToArray();

        return PagedResult<ClientDto>.CreateSuccess(users, totalItems, req.PageSize);
    }
}
