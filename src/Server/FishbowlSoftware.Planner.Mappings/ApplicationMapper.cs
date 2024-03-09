using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class ApplicationMapper
{
    public static ApplicationDto ToDto(this Application entity)
    {
        var dto = new ApplicationDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate
        };
        return dto;
    }
}
