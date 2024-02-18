using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Domain.Models.OralHealthExamination;

public class RiskFactorAssessment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public RiskFactorAssessmentModel RiskFactorAssessmentModel { get; set; }
    public PatientExaminationCard PatientExaminationCard { get; set; }
}
