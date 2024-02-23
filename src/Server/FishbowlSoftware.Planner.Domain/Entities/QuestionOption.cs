using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class QuestionOption : Entity
{
    public string? Text { get; set; }
    public string? QuestionId { get; set; }
    public Question? Question { get; set; }
}
