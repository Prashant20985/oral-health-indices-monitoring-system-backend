using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

/// <summary>
/// Input model for practice patient examination card.
/// </summary>
public record PracticePatientExaminationCardInputModel(
    CreatePatientDto PatientDto,
    SummaryRequestDto Summary,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    PracticeAPIDto PracticeAPI,
    PracticeBleedingDto PracticeBleeding,
    PracticeBeweDto PracticeBewe,
    PracticeDMFT_DMFSDto PracticeDMFT_DMFS);


