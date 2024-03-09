using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class ProjectSurveySubmission : Entity
{
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public string? SurveyId { get; set; }
    public Survey? Survey { get; set; }
    
    public List<SurveyQuestionResponse> QuestionResponses { get; set; } = [];
}
