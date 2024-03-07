using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Shared.Enums;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class ApplicationObject : AuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ApplicationObjectType ObjectType { get; set; }
    
    public string? UserStoryId { get; set; }
    public UserStory? UserStory { get; set; }
}
