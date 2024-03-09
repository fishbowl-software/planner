using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class SurveyMapper
{
    public static SurveyDto ToDto(this Survey entity)
    {
        var dto = new SurveyDto
        {
            Id = entity.Id,
            Title = entity.Title
        };
        return dto;
    }
}
