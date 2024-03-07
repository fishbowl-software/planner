using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Questionnaire : AuditableEntity
{
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
    public List<Question> Questions { get; set; } = [];
}
