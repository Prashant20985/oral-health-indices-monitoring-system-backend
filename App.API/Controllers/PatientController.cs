using App.Application.PatientOperations.Command.ArchivePatient;
using App.Application.PatientOperations.Command.CreatePatient;
using App.Application.PatientOperations.Command.DeletePatient;
using App.Application.PatientOperations.Command.UnarchivePatient;
using App.Application.PatientOperations.Command.UpdatePatient;
using App.Application.PatientOperations.Query.ActivePatients;
using App.Application.PatientOperations.Query.ArchivedPatients;
using App.Application.PatientOperations.Query.PatientDetails;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

/// <summary>
///  Represents the controller for patient operations.
/// </summary>
public class PatientController : BaseController
{
    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="createPatientDto">Data for creating a new patient.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpPost("create-patient")]
    public async Task<ActionResult> CreatePatient([FromBody] CreatePatientDto createPatient) =>
        HandleOperationResult(await Mediator.Send(new CreatePatientCommand(createPatient, User.FindFirstValue(ClaimTypes.NameIdentifier))));

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="patientId">ID of the patient to be updated.</param>
    /// <param name="updatePatient">Data for updating the patient.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpPut("update-patient/{patientId}")]
    public async Task<ActionResult> UpdatePatient(Guid patientId, [FromBody] UpdatePatientDto updatePatient) =>
        HandleOperationResult(await Mediator.Send(new UpdatePatientCommand(patientId, updatePatient)));

    /// <summary>
    /// Archives a patient.
    /// </summary>
    /// <param name="patientId">ID of the patient to be archived.</param>
    /// <param name="archiveComment">Comment for archiving the patient.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpPut("archive-patient/{patientId}")]
    public async Task<ActionResult> ArchivePatient(Guid patientId, string archiveComment) =>
        HandleOperationResult(await Mediator.Send(new ArchivePatientCommand(patientId, archiveComment)));

    /// <summary>
    /// Unarchives a patient.
    /// </summary>
    /// <param name="patientId">ID of the patient to be unarchived.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpPut("unarchive-patient/{patientId}")]
    public async Task<IActionResult> UnarchivePatient(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new UnarchivePatientCommand(patientId)));

    /// <summary>
    /// Deletes a patient.
    /// </summary>
    /// <param name="patientId">ID of the patient to be deleted.</param>
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-patient/{patientId}")]
    public async Task<IActionResult> DeletePatient(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new DeletePatientCommand(patientId)));

    /// <summary>
    /// Fetches all active patients.
    /// </summary>
    /// <param name="name">Name filter.</param>
    /// <param name="email">Email filter.</param>
    [Authorize]
    [HttpGet("active-patients")]
    public async Task<ActionResult<List<PatientResponseDto>>> FetchAllActivePatients(
        [FromQuery] string name,
        [FromQuery] string email,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20) => HandleOperationResult(await Mediator.Send(new FetchAllActivePatientsQuery(name, email, page, pageSize)));

    /// <summary>
    /// Fetches all archived patients.
    /// </summary>
    /// <param name="name">Name filter.</param>
    /// <param name="email">Email filter.</param>
    [Authorize]
    [HttpGet("archived-patients")]
    public async Task<ActionResult<List<PatientResponseDto>>> FetchAllArchivedPatients(
        [FromQuery] string name,
        [FromQuery] string email,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20) => HandleOperationResult(await Mediator.Send(new FetchAllArchivedPatientsQuery(name, email, page, pageSize)));

    /// <summary>
    /// Fetches patient details.
    /// </summary>
    /// <param name="patientId">ID of the patient to fetch details for.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpGet("patient-details/{patientId}")]
    public async Task<ActionResult<PatientResponseDto>> FetchPatientDetails(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientDetailsQuery(patientId)));

}
