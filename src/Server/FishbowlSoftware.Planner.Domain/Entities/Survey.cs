using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Survey : AuditableEntity
{
    public string? Title { get; set; }
    public List<SurveyQuestion> Questions { get; set; } = [];
}
