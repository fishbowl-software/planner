using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Domain.Specifications;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientsHandler : RequestHandler<GetClientsQuery, PagedResult<UserDto>>
{
    private readonly IUnitOfWork _uow;

    public GetClientsHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<PagedResult<UserDto>> HandleValidated(
        GetClientsQuery req, CancellationToken ct)
    {
        var totalItems = await _uow.Repository<User>().CountAsync();

        var users = _uow.Repository<User>()
            .ApplySpecification(new GetClientsPaged(req.OrderBy, req.Page, req.PageSize))
            .Select(i => i.ToDto())
            .ToArray();

        return PagedResult<UserDto>.CreateSuccess(users, totalItems, req.PageSize);
    }
}
