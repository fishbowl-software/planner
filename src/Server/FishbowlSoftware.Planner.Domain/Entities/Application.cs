using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Application : AuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public static Application Create(string name, string? description, Project project)
    {
        return new Application
        {
            Name = name,
            Description = description,
            Project = project,
            ProjectId = project.Id
        };
    }
}
