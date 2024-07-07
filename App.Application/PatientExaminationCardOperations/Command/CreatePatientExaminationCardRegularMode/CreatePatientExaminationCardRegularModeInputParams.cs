using App.Domain.DTOs.Common.Request;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Input parameters for creating patient examination card in regular mode
/// </summary>
/// <param name="AssignedDoctorId">The assigned doctor id</param>
/// <param name="PatientExaminationCardComment">The patient examination card comment</param>
/// <param name="RiskFactorAssessmentModel">The risk factor assessment model</param>
/// <param name="CreateDMFT_DMFSRequest">The create DMFT/DMFS request</param>
/// <param name="CreateBeweRequest">The create BEWE request</param>
/// <param name="CreateAPIRequest">The create API request</param>
/// <param name="CreateBleedingRequest">The create bleeding request</param>
public record CreatePatientExaminationCardRegularModeInputParams(
    string AssignedDoctorId,
    string PatientExaminationCardComment,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    CreateDMFT_DMFSRegularModeRequestDto CreateDMFT_DMFSRequest,
    CreateBeweRegularModeRequestDto CreateBeweRequest,
    CreateAPIRegularModeRequestDto CreateAPIRequest,
    CreateBleedingRegularModeRequestDto CreateBleedingRequest);
