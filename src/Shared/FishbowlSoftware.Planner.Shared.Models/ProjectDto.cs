namespace FishbowlSoftware.Planner.Shared.Models;

public class ProjectDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; } 
    public ClientDto? Client { get; set; }
}
