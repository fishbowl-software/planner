﻿using FishbowlSoftware.Planner.Shared;
using FishbowlSoftware.Planner.Shared.Models;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Queries;

public class GetClientQuery : IRequest<Result<ClientDto>>
{
    public required string Id { get; set; }
}
