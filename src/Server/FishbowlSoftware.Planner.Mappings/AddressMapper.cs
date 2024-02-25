using FishbowlSoftware.Planner.Domain.ValueObjects;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class AddressMapper
{
    public static AddressDto ToDto(this Address entity)
    {
        var dto = new AddressDto
        {
            Line1 = entity.Line1,
            Line2 = entity.Line2,
            City = entity.City,
            ZipCode = entity.ZipCode,
            Region = entity.Region,
            Country = entity.Country
        };
        return dto;
    }
}
