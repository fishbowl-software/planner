using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class FlowMapper
{
    public static FlowDto ToDto(this Flow entity)
    {
        var dto = new FlowDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate
        };
        return dto;
    }
}
