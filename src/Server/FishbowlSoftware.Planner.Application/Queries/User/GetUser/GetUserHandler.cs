﻿using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Mappings;
using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Application.Queries;

internal class GetUserHandler : RequestHandler<GetUserQuery, Result<UserDto>>
{
    private readonly IUnitOfWork _uow;

    public GetUserHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result<UserDto>> HandleValidated(
        GetUserQuery req, CancellationToken ct)
    {
        var userEntity = await _uow.Repository<User>()
            .GetAsync(i => i.Id == req.Id || i.Email == req.Id);

        if (userEntity is null)
        {
            return Result<UserDto>.CreateFailure($"Could not find a client with ID {req.Id}");
        }

        var userDto = userEntity.ToDto();
        return Result<UserDto>.CreateSuccess(userDto);
    }
}
