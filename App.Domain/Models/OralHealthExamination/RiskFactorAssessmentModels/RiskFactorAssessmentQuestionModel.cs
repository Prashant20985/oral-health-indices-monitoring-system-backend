namespace App.Domain.Models.OralHealthExamination.RiskFactorAssessmentModels;

public class RiskFactorAssessmentQuestionModel
{
    public string QuestionText { get; set; }
    public RiskFactorAssessmentAnswerModel Answer { get; set; }
}
