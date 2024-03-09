using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings;

public static class QuestionMapper
{
    public static QuestionDto ToDto(this SurveyQuestion entity)
    {
        var dto = new QuestionDto
        {
            Id = entity.Id,
            Text = entity.Text
        };
        return dto;
    }
}
