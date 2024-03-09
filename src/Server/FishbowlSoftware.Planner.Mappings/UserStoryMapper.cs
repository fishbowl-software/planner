using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class UserStoryMapper
{
    public static UserStoryDto ToDto(this UserStory entity)
    {
        var dto = new UserStoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedDate = entity.CreatedDate
        };
        return dto;
    }
}
