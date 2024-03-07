using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class UserStory : AuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public string? FlowId { get; set; }
    public Flow? Flow { get; set; }
    
    public List<ApplicationObject> ApplicationObjects { get; set; } = [];
}
