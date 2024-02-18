using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.Models.OralHealthExamination;

public class DMFT_DMFS
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal DMFTResult { get; private set; }
    public decimal DMFSResult { get; private set; }
    public string Comment { get; private set; }

    public DMFT_DMFSAssessmentModel AssessmentModel { get; set; }

    public PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comment = comment;
}
