using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Client : AuditableEntity
{
    public string? Name { get; set; }
    
    public string? UserId { get; set; }
    public User? User { get; set; }
    
    public List<Project> Projects { get; set; } = [];

    public static Client Create(string name, User? user = null)
    {
        var newClient = new Client
        {
            Name = name,
            User = user,
            UserId = user?.Id
        };

        return newClient;
    }
}
