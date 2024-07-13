using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.ExamDtos.Request;

public class PracticeDMFT_DMFSDto
{
    public string ProstheticStatus { get; set; }
    public decimal DMFTResult { get; set; }
    public decimal DMFSResult { get; set; }
    public DMFT_DMFSAssessmentModel AssessmentModel { get; set; }
}
