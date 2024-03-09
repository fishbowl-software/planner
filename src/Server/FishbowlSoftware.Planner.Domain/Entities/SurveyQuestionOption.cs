using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class SurveyQuestionOption : AuditableEntity
{
    public string? Text { get; set; }
    public string? QuestionId { get; set; }
    public SurveyQuestion? Question { get; set; }
}
