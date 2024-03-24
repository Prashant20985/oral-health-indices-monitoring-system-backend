using App.Application.PatientOperations.Command.ArchivePatient;
using App.Application.PatientOperations.Command.CreatePatient;
using App.Application.PatientOperations.Command.DeletePatient;
using App.Application.PatientOperations.Command.UnarchivePatient;
using App.Application.PatientOperations.Command.UpdatePatient;
using App.Application.PatientOperations.Query.ActivePatients;
using App.Application.PatientOperations.Query.ActivePatientsByDoctorId;
using App.Application.PatientOperations.Query.ArchivedPatients;
using App.Application.PatientOperations.Query.ArchivedPatientsByDoctorId;
using App.Application.PatientOperations.Query.PatientDetails;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

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
    [Authorize(Roles = "Admin")]
    [HttpGet("active-patients")]
    public async Task<ActionResult<List<PatientDto>>> FetchAllActivePatients([FromQuery] string name, [FromQuery] string email) =>
        HandleOperationResult(await Mediator.Send(new FetchAllActivePatientsQuery(name, email)));

    /// <summary>
    /// Fetches all archived patients.
    /// </summary>
    /// <param name="name">Name filter.</param>
    /// <param name="email">Email filter.</param>
    [Authorize(Roles = "Admin")]
    [HttpGet("archived-patients")]
    public async Task<ActionResult<List<PatientDto>>> FetchAllArchivedPatients([FromQuery] string name, [FromQuery] string email) =>
        HandleOperationResult(await Mediator.Send(new FetchAllArchivedPatientsQuery(name, email)));

    /// <summary>
    /// Fetches all active patients by Doctor ID.
    /// </summary>
    /// <param name="name">Name filter.</param>
    /// <param name="email">Email filter.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpGet("active-patients-by-doctorId")]
    public async Task<ActionResult<List<PatientDto>>> FetchAllActivePatientsByDoctorId([FromQuery] string name, [FromQuery] string email) =>
        HandleOperationResult(await Mediator.Send(new FetchAllActivePatientsByDoctorIdQuery(User.FindFirstValue(ClaimTypes.NameIdentifier), name, email)));

    /// <summary>
    /// Fetches all archived patients by Doctor ID.
    /// </summary>
    /// <param name="name">Name filter.</param>
    /// <param name="email">Email filter.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpGet("archived-patients-by-doctorId")]
    public async Task<ActionResult<List<PatientDto>>> FetchAllArchivedPatientsByDoctorId([FromQuery] string name, [FromQuery] string email) =>
        HandleOperationResult(await Mediator.Send(new FetchAllArchivedPatientsByDoctorIdQuery(User.FindFirstValue(ClaimTypes.NameIdentifier), name, email)));

    /// <summary>
    /// Fetches patient details.
    /// </summary>
    /// <param name="patientId">ID of the patient to fetch details for.</param>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner, Student")]
    [HttpGet("patient-details/{patientId}")]
    public async Task<ActionResult<PatientDto>> FetchPatientDetails(Guid patientId) =>
        HandleOperationResult(await Mediator.Send(new FetchPatientDetailsQuery(patientId)));

}
