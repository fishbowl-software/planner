namespace FishbowlSoftware.Planner.Shared.Models;

public class SurveyDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? ProjectId { get; set; }
    public List<QuestionDto> Questions { get; set; } = [];
}
