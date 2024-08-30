using App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;
using App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;
using App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;
using App.Application.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;
using App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByStudent;
using App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;
using App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;
using App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;
using App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;
using App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCards;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.RiskFactorAssessment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;
/// <summary>
///  Represents the controller for patient examination card operations.
/// </summary>
public class PatientExaminationCardController : BaseController
{
    /// <summary>
    /// Add comment to patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-patient-examination-card/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<IActionResult> CommentPatientExaminationCard(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentPatientExaminationCardCommand(cardId, comment, User.IsInRole("Student"))));

    /// <summary>
    /// Add comment to API form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-api-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<IActionResult> CommentAPIForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentAPIFormCommnand(cardId, comment, User.IsInRole("Student"))));

    /// <summary>
    /// Add comment to DMFT_DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-bewe-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<IActionResult> CommentBeweForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentBeweFormCommand(cardId, comment, User.IsInRole("Student"))));

    /// <summary>
    /// Add comment to bleeding form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-bleeding-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<IActionResult> CommentBleedingForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentBleedingFormCommand(cardId, comment, User.IsInRole("Student"))));

    /// <summary>
    /// Add comment to DMFT_DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="doctorComment">Comment of the doctor</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("comment-dmft-dmfs-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<IActionResult> CommentDMFTDMFSForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentDMFT_DMFSCommand(cardId, comment, User.IsInRole("Student"))));

    /// <summary>
    /// Update API form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">API form model</param>
    /// <returns>An ActionResult of APIResultResponseDto</returns>
    [HttpPut("update-api-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<APIResultResponseDto>> UpdateAPIForm(Guid cardId, [FromBody] APIBleedingAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateAPIFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update DMFT/DMFS form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">DMFT/DMFS form model</param>
    /// <returns>An ActionResult of DMFT_DMFSResultResponseDto</returns>
    [HttpPut("update-dmft-dmfs-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<DMFT_DMFSResultResponseDto>> UpdateDMFTDMFSForm(Guid cardId, string prostheticStatus, [FromBody] UpdateDMFT_DMFSRequestDto updateDMFT_DMFS) =>
        HandleOperationResult(await Mediator.Send(new UpdateDMFT_DMFSFormCommand(cardId, updateDMFT_DMFS)));

    /// <summary>
    /// Update bleeding form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">Bleeding form model</param>
    /// <returns>An ActionResult of BleedingResultResponseDto</returns>
    [HttpPut("update-bleeding-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<BleedingResultResponseDto>> UpdateBleedingForm(Guid cardId, [FromBody] APIBleedingAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateBleedingFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update BEWE form
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">BEWE form model</param>
    /// <returns>An ActionResult of decimal</returns>
    [HttpPut("update-bewe-form/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<BeweResultResponseDto>> UpdateBeweForm(Guid cardId, [FromBody] BeweAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateBeweFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Update risk factor assessment
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="assessmentModel">Ris factor assessment form model</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("update-risk-factor-assessment/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<IActionResult> UpdateRiskFactorAssessment(Guid cardId, [FromBody] RiskFactorAssessmentModel assessmentModel) =>
        HandleOperationResult(await Mediator.Send(new UpdateRiskFactorAssessmentFormCommand(cardId, assessmentModel)));

    /// <summary>
    /// Create patient examination card in regular mode by doctor
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <param name="inputParams">Input parameters</param>
    /// <returns>An ActionResult of PatientExaminationCardDto</returns>
    [HttpPost("create-patient-examination-card-by-doctor/{patientId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<PatientExaminationCardDto>> CreatePatientExaminationCardByDoctor(
        Guid patientId,
        [FromBody] CreatePatientExaminationCardByDoctorInputParams inputParams) =>
            HandleOperationResult(await Mediator.Send(new CreatePatientExaminationCardByDoctorCommand(
                        patientId,
                        User.FindFirstValue(ClaimTypes.NameIdentifier),
                        inputParams)));

    /// <summary>
    /// Create patient examination card in test mode by student
    /// </summary>
    /// <param name="patientId"></param>
    /// <param name="inputParams"></param>
    /// <returns></returns>
    [HttpPost("create-patient-examination-card-by-student/{patientId}")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<PatientExaminationCardDto>> CreatePatientExaminationCardByStudent(
        Guid patientId,
        [FromBody] CreatePatientExaminationCardByStudentInputParams inputParams) =>
            HandleOperationResult(await Mediator.Send(new CreatePatientExaminationCardByStudentCommand(
                        patientId,
                        User.FindFirstValue(ClaimTypes.NameIdentifier),
                        inputParams)));

    /// <summary>
    /// Delete patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <returns>An IActionResult</returns>
    [HttpDelete("delete-patient-examination-card/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<IActionResult> DeletePatientExaminationCard(Guid cardId) =>
        HandleOperationResult(await Mediator.Send(new DeletePatientExaminationCardCommand(cardId)));

    /// <summary>
    /// Grade patient examination card
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="totalScore">Total score of the card</param>
    /// <returns>An IActionResult</returns>
    [HttpPut("grade-patient-examination-card/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<IActionResult> GradePatientExaminationCard(Guid cardId, decimal totalScore) =>
        HandleOperationResult(await Mediator.Send(new GradePatientExaminationCardCommand(cardId, totalScore)));

    /// <summary>
    /// Get patient examination card details
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <returns>An ActionResult of PatientExaminationCardDto</returns>
    [HttpGet("patient-examination-card/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<ActionResult<PatientExaminationCardDto>> GetPatientExaminationCardDetails(Guid cardId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientExaminationCardDetailsQuery(cardId)));

    /// <summary>
    /// Gets all patient examination cards in regular mode
    /// </summary>
    /// <param name="patientId">Unique identifier of the patient</param>
    /// <returns>An ActionResult of List of PatientExaminationCardDto</returns>
    [HttpGet("patient-examination-cards/{patientId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    public async Task<ActionResult<List<PatientExaminationCardDto>>> GetPatientExaminationCards(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientExaminationCardsQuery(patientId)));

    /// <summary>
    /// Gets all patient examination cards assigned to doctor
    /// </summary>
    /// <param name="doctorId">Unique identifier of the doctor</param>
    /// <param name="studentId">Unique identifier of the student</param>
    /// <param name="year">Year of the examination card</param>
    /// <param name="month">Month of the examination card</param>
    /// <returns>An ActionResult of List of PatientExaminationCardWithPatientDetailsDto</returns>
    [HttpGet("patient-examination-cards-assigned-to-doctor")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult<List<PatientDetailsWithExaminationCards>>> GetPatientExaminationCardsAssignedToDoctor(
        string studentId,
        int year,
        int month) => HandleOperationResult(
            await Mediator.Send(new FetchPatientExaminationCardsAssignedToDoctorQuery(User.FindFirstValue(ClaimTypes.NameIdentifier), studentId, year, month)));

    /// <summary>
    /// Update patient examination card summary
    /// </summary>
    /// <param name="cardId">Unique identifier of the card</param>
    /// <param name="summary">Summary of the card</param>
    /// <returns>An ActionResult</returns>
    [HttpPut("update-patient-examination-card-summary/{cardId}")]
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    public async Task<ActionResult> UpdatePatientExaminationCardSummary(Guid cardId, [FromBody] SummaryRequestDto summary) =>
        HandleOperationResult(await Mediator.Send(new UpdatePatientExaminationCardSummaryCommand(cardId, summary)));

}
