using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Questionnaire : Entity
{
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
}
