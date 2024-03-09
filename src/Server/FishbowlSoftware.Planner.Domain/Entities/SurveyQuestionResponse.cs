using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class SurveyQuestionResponse : Entity
{
    public string? ProjectSurveySubmissionId { get; set; }
    public ProjectSurveySubmission? ProjectSurveySubmission { get; set; }
    
    public string? QuestionId { get; set; }
    public SurveyQuestion? Question { get; set; }
    
    public string? OptionId { get; set; }
    public SurveyQuestionOption? Option { get; set; }
}
