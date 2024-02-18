namespace App.Domain.Models.Common.RiskFactorAssessment;

/// <summary>
/// Represents a model for a risk factor assessment question.
/// </summary>
public class RiskFactorAssessmentQuestionModel
{
    /// <summary>
    /// Gets or sets the text of the assessment question.
    /// </summary>
    public string QuestionText { get; set; }

    /// <summary>
    /// Gets or sets the answer options for the assessment question.
    /// </summary>
    public RiskFactorAssessmentAnswerModel Answer { get; set; }
}

