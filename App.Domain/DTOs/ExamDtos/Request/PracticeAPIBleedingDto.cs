using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.ExamDtos.Request;

public class PracticeAPIBleedingDto
{
    public decimal APIResult { get; set; }
    public decimal BleedingResult { get; set; }
    public APIBleedingAssessmentModel AssessmentModel { get; set; }
}
