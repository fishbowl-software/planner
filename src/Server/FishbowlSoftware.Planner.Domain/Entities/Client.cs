using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Client : Entity
{
    public string? Name { get; set; }
    public List<Project> Projects { get; set; } = [];
}
