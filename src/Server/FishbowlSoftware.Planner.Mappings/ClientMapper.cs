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
            AccountId = entity.AccountId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Organization = entity.Organization,
            Address = entity.Address.ToDto()
        };
        return dto;
    }
}
