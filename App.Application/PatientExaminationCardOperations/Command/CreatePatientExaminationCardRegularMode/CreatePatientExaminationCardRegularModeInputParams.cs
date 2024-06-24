using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Input parameters for creating patient examination card in regular mode
/// </summary>
/// <param name="DoctorId">Doctor ID</param>
/// <param name="RiskFactorAssessmentModel">Risk factor assessment model</param>
/// <param name="DMFT_DMFSAssessmentModel">DMFT_DMFS assessment model</param>
/// <param name="BeweAssessmentModel">Bewe assessment model</param>
/// <param name="APIAssessmentModel">API assessment model</param>
/// <param name="BleedingAssessmentModel">Bleeding assessment model</param>
public record CreatePatientExaminationCardRegularModeInputParams(
    string DoctorId,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    DMFT_DMFSAssessmentModel DMFT_DMFSAssessmentModel,
    BeweAssessmentModel BeweAssessmentModel,
    APIBleedingAssessmentModel APIAssessmentModel,
    APIBleedingAssessmentModel BleedingAssessmentModel);
