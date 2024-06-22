using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.ExamDtos.Request;

public class PracticeBleedingDto
{
    public int BleedingResult { get; set; }
    public int Maxilla { get; set; }
    public int Mandible { get; set; }
    public APIBleedingAssessmentModel AssessmentModel { get; set; }
}
