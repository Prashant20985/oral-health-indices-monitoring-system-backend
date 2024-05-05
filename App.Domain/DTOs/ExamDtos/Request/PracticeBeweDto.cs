using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.ExamDtos.Request;

public class PracticeBeweDto
{
    public decimal BeweResult { get; set; }
    public BeweAssessmentModel AssessmentModel { get; set; }
}
