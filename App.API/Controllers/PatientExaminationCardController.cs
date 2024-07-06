using App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;
using App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;
using App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;
using App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;
using App.Application.PatientExaminationCardOperations.Query.FetchAllPatientExaminationCardsInRegualrMode;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardInTestMode;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
public class PatientExaminationCardController : BaseController
{
    /// <summary>
    /// Add comment to patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-patient-examination-card/{cardId}")]
    public async Task<IActionResult> CommentPatientExaminationCard(Guid cardId, string doctorComment) =>
        HandleOperationResult(await Mediator.Send(new CommentPatientExaminationCardCommand(cardId, doctorComment)));

    /// <summary>
    /// Add comment to API form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-api-form/{cardId}")]
    public async Task<IActionResult> CommentAPIForm(Guid cardId, string doctorComment) =>
        HandleOperationResult(await Mediator.Send(new CommentAPIFormCommand(cardId, doctorComment)));

    /// <summary>
    /// Add comment to DMFT_DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-bewe-form/{cardId}")]
    public async Task<IActionResult> CommentBeweForm(Guid cardId, string doctorComment) =>
        HandleOperationResult(await Mediator.Send(new CommentBeweFormCommand(cardId, doctorComment)));

    /// <summary>
    /// Add comment to bleeding form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-bleeding-form/{cardId}")]
    public async Task<IActionResult> CommentBleedingForm(Guid cardId, string doctorComment) =>
        HandleOperationResult(await Mediator.Send(new CommentBleedingFormCommand(cardId, doctorComment)));

    /// <summary>
    /// Add comment to DMFT_DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-dmft-dmfs-form/{cardId}")]
    public async Task<IActionResult> CommentDMFTDMFSForm(Guid cardId, string doctorComment) =>
        HandleOperationResult(await Mediator.Send(new CommentDMFT_DMFSFormCommand(cardId, doctorComment)));

    /// <summary>
    /// Update API form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">API form model</param>
    /// <returns>An ActionResult of APIResultResponseDto</returns>
    [HttpPut("update-api-form/{cardId}")]
    public async Task<ActionResult<APIResultResponseDto>> UpdateAPIForm(Guid cardId, [FromBody] APIBleedingAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateAPIFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update DMFT/DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">DMFT/DMFS form model</param>
    /// <returns>An ActionResult of DMFT_DMFSResultResponseDto</returns>
    [HttpPut("update-dmft-dmfs-form/{cardId}")]
    public async Task<ActionResult<DMFT_DMFSResultResponseDto>> UpdateDMFTDMFSForm(Guid cardId, [FromBody] DMFT_DMFSAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateDMFT_DMFSFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update bleeding form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">Bleeding form model</param>
    /// <returns>An ActionResult of BleedingResultResponseDto</returns>
    [HttpPut("update-bleeding-form/{cardId}")]
    public async Task<ActionResult<BleedingResultResponseDto>> UpdateBleedingForm(Guid cardId, [FromBody] APIBleedingAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateBleedingFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update BEWE form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">BEWE form model</param>
    /// <returns>An ActionResult of decimal</returns>
    [HttpPut("update-bewe-form/{cardId}")]
    public async Task<ActionResult<decimal>> UpdateBeweForm(Guid cardId, [FromBody] BeweAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateBeweFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update risk factor assessment
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">Ris factor assessment form model</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("update-risk-factor-assessment/{cardId}")]
    public async Task<IActionResult> UpdateRiskFactorAssessment(Guid cardId, [FromBody] RiskFactorAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateRiskFactorAssessmentFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Create patient examination card in regular mode
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <param name="inputParams">Input parameters</param>
    /// <returns>An ActionResult of PatientExaminationCardDto</returns>
    [HttpPost("create-patient-examination-card-regular-mode/{patientId}")]
    public async Task<ActionResult<PatientExaminationCardDto>> CreatePatientExaminationCardRegularMode(Guid patientId,
        [FromBody] CreatePatientExaminationCardRegularModeInputParams inputParams) =>
            HandleOperationResult(await Mediator.Send(new CreatePatientExaminationCardRegularModeCommand(patientId, inputParams)));

    /// <summary>
    /// Create patient examination card in test mode
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <param name="inputParams">Input parameters</param>
    /// <returns>An ActionResult of PatientExaminationCardDto</returns>
    [HttpPost("create-patient-examination-card-test-mode/{patientId}")]
    public async Task<ActionResult<PatientExaminationCardDto>> CreatePatientExaminationCardTestMode(Guid patientId,
               [FromBody] CreatePatientExaminationCardTestModeInputParams inputParams) =>
            HandleOperationResult(await Mediator.Send(new CreatePatientExaminationCardTestModeCommand(patientId, inputParams)));

    /// <summary>
    /// Delete patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <returns>An IActionResult</returns>
    [HttpDelete("delete-patient-examination-card/{cardId}")]
    public async Task<IActionResult> DeletePatientExaminationCard(Guid cardId) =>
        HandleOperationResult(await Mediator.Send(new DeletePatientExaminationCardCommand(cardId)));

    /// <summary>
    /// Grade patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="totalScore">Total score of the card</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("grade-patient-examination-card/{cardId}")]
    public async Task<IActionResult> GradePatientExaminationCard(Guid cardId, decimal totalScore) =>
        HandleOperationResult(await Mediator.Send(new GradePatientExaminationCardCommand(cardId, totalScore)));

    /// <summary>
    /// Get patient examination card details
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <returns>An ActionResult of PatientExaminationCardDto</returns>
    [HttpGet("get-patient-examination-card/{cardId}")]
    public async Task<ActionResult<PatientExaminationCardDto>> GetPatientExaminationCardDetails(Guid cardId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientExaminationCardDetailsQuery(cardId)));

    /// <summary>
    /// Gets all patient examination cards in regular mode
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <returns>An ActionResult of List of PatientExaminationCardDto</returns>
    [HttpGet("get-patient-examination-cards-regular-mode-by-patient/{patientId}")]
    public async Task<ActionResult<List<PatientExaminationCardDto>>> GetPatientExaminationCardsRegularModeByPatient(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientExaminationCardsInRegularModeQuery(patientId)));

    /// <summary>
    /// Gets all patient examination cards in test mode
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <returns>An ActionResult of List of PatientExaminationCardDto</returns>
    [HttpGet("get-patient-examination-cards-test-mode-by-patient/{patientId}")]
    public async Task<ActionResult<List<PatientExaminationCardDto>>> GetPatientExaminationCardsTestModeByPatient(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientExaminationCardsInTestModeQuery(patientId)));
}
