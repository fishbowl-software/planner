using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Application : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? ClientId { get; set; }
    public Client? Client { get; set; }
    
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
}
