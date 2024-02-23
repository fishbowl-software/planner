using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Flow : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public List<UserStory> UserStories { get; set; } = [];
}
