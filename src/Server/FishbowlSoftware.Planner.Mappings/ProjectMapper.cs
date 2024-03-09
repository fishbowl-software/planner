using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class ProjectMapper
{
    public static ProjectDto ToDto(this Project entity)
    {
        var dto = new ProjectDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Client = entity.Client?.ToDto(),
            CreatedDate = entity.CreatedDate
        };
        return dto;
    }
}
