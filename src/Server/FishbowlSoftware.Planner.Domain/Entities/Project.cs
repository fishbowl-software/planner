using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Project : AuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? ClientId { get; set; }
    public Client? Client { get; set; }
    
    public List<ProjectSurveySubmission> SurveySubmissions { get; set; } = [];
    public List<Application> Applications { get; set; } = [];
    public List<Flow> Flows { get; set; } = [];
    
    public static Project Create(
        string name,
        string? description,
        Client client)
    {
        var project = new Project
        {
            Name = name,
            Description = description,
            Client = client,
            ClientId = client.Id
        };
        
        return project;
    }
}
