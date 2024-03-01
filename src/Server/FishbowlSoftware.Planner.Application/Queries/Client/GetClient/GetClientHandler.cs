using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetClientHandler : RequestHandler<GetClientQuery, Result<UserDto>>
{
    private readonly IUnitOfWork _uow;

    public GetClientHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result<UserDto>> HandleValidated(
        GetClientQuery req, CancellationToken ct)
    {
        var userEntity = await _uow.Repository<User>()
            .GetAsync(i => i.Id == req.Id || i.Email == req.Id);

        if (userEntity is null)
        {
            return Result<UserDto>.CreateError($"Could not find a client with ID {req.Id}");
        }

        var userDto = userEntity.ToDto();
        return Result<UserDto>.CreateSuccess(userDto);
    }
}
