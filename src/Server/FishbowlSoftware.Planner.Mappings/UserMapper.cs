using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class UserMapper
{
    public static UserDto ToDto(this User entity)
    {
        var dto = new UserDto
        {
            Id = entity.Id,
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
