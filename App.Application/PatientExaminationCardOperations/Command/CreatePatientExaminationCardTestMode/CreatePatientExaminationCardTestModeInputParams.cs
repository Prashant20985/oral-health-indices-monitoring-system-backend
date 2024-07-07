using App.Domain.DTOs.Common.Request;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

/// <summary>
/// Input parameters for creating a patient examination card in test mode
/// </summary>
/// <param name="DoctorId">The ID of the doctor</param>
/// <param name="PatientExaminationCardCommentByStudent">The comment by the student</param>
/// <param name="RiskFactorAssessmentModel">The risk factor assessment model</param>
/// <param name="CreateDMFT_DMFSRequest">The request to create a DMFT/DMFS assessment</param>
/// <param name="CreateBeweRequest">The request to create a BEWE assessment</param>
/// <param name="CreateAPIRequest">The request to create an API assessment</param>
/// <param name="CreateBleedingRequest">The request to create a bleeding assessment</param>
public record CreatePatientExaminationCardTestModeInputParams(
    string DoctorId,
    string PatientExaminationCardCommentByStudent,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    CreateDMFT_DMFSTestModeRequestDto CreateDMFT_DMFSRequest,
    CreateBeweTestModeRequestDto CreateBeweRequest,
    CreateAPITestModeRequestDto CreateAPIRequest,
    CreateBleedingTestModeRequestDto CreateBleedingRequest);

