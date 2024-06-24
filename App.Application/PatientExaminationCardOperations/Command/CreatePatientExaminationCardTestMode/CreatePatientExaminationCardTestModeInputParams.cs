using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

/// <summary>
/// Input parameters for creating a patient examination card in test mode
/// </summary>
/// <param name="DoctorId">The ID of the doctor who is assigned to the patient examination card</param>
/// <param name="StudentId">The ID of the student who is creating the patient examination card</param>
/// <param name="APIResult">Result of the API assessment</param>
/// <param name="BleedingResult">Result of the bleeding assessment</param>
/// <param name="BeweResult">Result of the BEWE assessment</param>
/// <param name="DMFT_Result">Result of the DMFT assessment</param>
/// <param name="DMFS_Result">Result of the DMFS assessment</param>
/// <param name="RiskFactorAssessmentModel">Assessment model for the risk factors</param>
/// <param name="DMFT_DMFSAssessmentModel">Assessment model for the DMFT/DMFS</param>
/// <param name="BeweAssessmentModel">Assessment model for the BEWE</param>
/// <param name="APIAssessmentModel">Assessment model for the API</param>
/// <param name="BleedingAssessmentModel">Assessment model for the bleeding</param>
public record CreatePatientExaminationCardTestModeInputParams(
    string DoctorId,
    string StudentId,
    int APIResult,
    int BleedingResult,
    decimal BeweResult,
    decimal DMFT_Result,
    decimal DMFS_Result,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    DMFT_DMFSAssessmentModel DMFT_DMFSAssessmentModel,
    BeweAssessmentModel BeweAssessmentModel,
    APIBleedingAssessmentModel APIAssessmentModel,
    APIBleedingAssessmentModel BleedingAssessmentModel);

