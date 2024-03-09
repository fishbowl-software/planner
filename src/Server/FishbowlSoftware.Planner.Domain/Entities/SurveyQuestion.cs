using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class SurveyQuestion : AuditableEntity
{
    public string? Text { get; set; }
    public string? SurveyId { get; set; }
    public Survey? Survey { get; set; }
    public List<SurveyQuestionOption> Options { get; set; } = [];
}
