using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Project : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public string? ClientId { get; set; }
    public User? Client { get; set; }
    
    public string? QuestionnaireId { get; set; }
    public Questionnaire? Questionnaire { get; set; }
    
    public List<Application> Applications { get; set; } = [];
    public List<Flow> Flows { get; set; } = [];
}
