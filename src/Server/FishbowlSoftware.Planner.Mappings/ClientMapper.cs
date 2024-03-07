using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class ClientMapper
{
    public static ClientDto ToDto(this Client entity)
    {
        var dto = new ClientDto
        {
            Id = entity.Id,
            Name = entity.Name,
            User = entity.User?.ToDto(),
            CreatedDate = entity.CreatedDate
        };
        return dto;
    }
}
