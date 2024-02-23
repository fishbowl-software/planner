using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Question : Entity
{
    public string? Text { get; set; }
    public string? QuestionnaireId { get; set; }
    public Questionnaire? Questionnaire { get; set; }
    public List<QuestionOption> Options { get; set; } = [];
}
