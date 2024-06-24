using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.Models.OralHealthExamination;

public class DMFT_DMFS
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal DMFTResult { get; private set; }
    public decimal DMFSResult { get; private set; }
    public string Comment { get; private set; }

    public DMFT_DMFSAssessmentModel AssessmentModel { get; private set; }

    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comment = comment;

    public void SetDMFTResult(decimal dmftResult) => DMFTResult = dmftResult;

    public void SetDMFSResult(decimal dmfsResult) => DMFSResult = dmfsResult;

    public void SetDMFT_DMFSAssessmentModel(DMFT_DMFSAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    // TODO
    public void CalculateDMFTResult() => DMFTResult = 0;
    public void CalculateDMFSResult() => DMFSResult = 0;
}
