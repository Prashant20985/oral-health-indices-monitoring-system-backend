using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

/// <summary>
/// Input model for practice patient examination card.
/// </summary>
public record PracticePatientExaminationCardInputModel(
    CreatePatientDto PatientDto,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    PracticeAPIBleedingDto PracticeAPIBleeding,
    PracticeBeweDto PracticeBewe,
    PracticeDMFT_DMFSDto PracticeDMFT_DMFS);


