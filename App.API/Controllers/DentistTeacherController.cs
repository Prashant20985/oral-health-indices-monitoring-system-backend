using App.Application.DentistTeacherOperations.Command.AddPatientToResearchGroup;
using App.Application.DentistTeacherOperations.Command.AddStudentToGroup;
using App.Application.DentistTeacherOperations.Command.CreateGroup;
using App.Application.DentistTeacherOperations.Command.CreateResearchGroup;
using App.Application.DentistTeacherOperations.Command.DeleteGroup;
using App.Application.DentistTeacherOperations.Command.DeleteResearchGroup;
using App.Application.DentistTeacherOperations.Command.RemovePatientFromResearchGroup;
using App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;
using App.Application.DentistTeacherOperations.Command.UpdateGroupName;
using App.Application.DentistTeacherOperations.Command.UpdateResearchGroup;
using App.Application.DentistTeacherOperations.Query.Groups;
using App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;
using App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;
using App.Application.DentistTeacherOperations.Query.ResearchGroups;
using App.Application.DentistTeacherOperations.Query.StudentGroupDetails;
using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace App.API.Controllers;

public class DentistTeacherController : BaseController
{
    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="GroupName">The name of the group to create.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpPost("create-group")]
    public async Task<ActionResult> CreateGroup(string GroupName) => HandleOperationResult(
        await Mediator.Send(new CreateGroupCommand(GroupName, User.FindFirstValue(ClaimTypes.NameIdentifier))));


    /// <summary>
    /// Adds a student to a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group to which the student will be added.</param>
    /// <param name="studentId">The identifier of the student to add to the group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpPost("add-student/{groupId}")]
    public async Task<ActionResult> AddStudentToGroup(Guid groupId, string studentId) => HandleOperationResult(
        await Mediator.Send(new AddStudentToGroupCommand(groupId, studentId)));


    /// <summary>
    /// Removes a student from a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group from which the student will be removed.</param>
    /// <param name="studentId">The identifier of the student to remove from the group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpDelete("remove-student/{groupId}")]
    public async Task<ActionResult> RemoveStudentFromGroup(Guid groupId, string studentId) => HandleOperationResult(
    await Mediator.Send(new RemoveStudentFromGroupCommand(groupId, studentId)));

    /// <summary>
    /// Deletes group
    /// </summary>
    /// <param name="groupId">The identifier of the group which will be deleted.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpDelete("delete-group/{groupId}")]
    public async Task<ActionResult> DeleteGroup(Guid groupId) => HandleOperationResult(
        await Mediator.Send(new DeleteGroupCommand(groupId)));

    /// <summary>
    /// Updates name of group
    /// </summary>
    /// <param name="groupId">The identifier of the group whch groups name will be updated</param>
    /// <param name="groupName"></param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpPut("update-groupname/{groupId}")]
    public async Task<IActionResult> UpdateGroupName(Guid groupId, string groupName) => HandleOperationResult(
        await Mediator.Send(new UpdateGroupNameCommand(groupId, groupName)));

    /// <summary>
    /// fetches students not in the group
    /// </summary>
    /// <param name="groupId">The identiffier of the group in which students are not present</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpGet("get-studentsNotInGroup/{groupId}")]
    public async Task<IActionResult> GetStudentsNotInGroup([Required] Guid groupId) => HandleOperationResult(
        await Mediator.Send(new FetchStudentsNotInGroupListQuery(groupId)));

    /// <summary>
    /// Retrieves a list of all groups associated with the currently authentcated teacher. 
    /// </summary>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpGet("groups")]
    public async Task<ActionResult<List<GroupDto>>> GetAllGroups() => HandleOperationResult(
        await Mediator.Send(new FetchGroupsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier))));

    /// <summary>
    /// Retrieves details about a student group by group id.
    /// </summary>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
    [HttpGet("group-details/{groupId}")]
    public async Task<ActionResult<GroupDto>> GetGroupDetails(Guid groupId) => HandleOperationResult(
               await Mediator.Send(new FetchStudentGroupQuery(groupId)));

    /// <summary>
    /// Retrieves a list of all research groups.
    /// </summary>
    /// <param name="groupName">Filter by group name (optional).</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpGet("research-groups")]
    public async Task<ActionResult<List<ResearchGroupDto>>> GetAllResearchGroups(string groupName) => HandleOperationResult(
        await Mediator.Send(new FetchResearchGroupsQuery(groupName)));

    /// <summary>
    /// Retrieves a list of patients not in any research group.
    /// </summary>
    /// <param name="patientName">Filter by patient name (optional).</param>
    /// <param name="email">Filter by patient email (optional).</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpGet("patients-not-in-research-group")]
    public async Task<ActionResult<List<ResearchGroupPatientDto>>> GetPatientsNotInResearchGroup(string patientName,
        string email) => HandleOperationResult(
                  await Mediator.Send(new FetchPatientsNotInResearchGroupsQuery(patientName, email)));

    /// <summary>
    /// Creates a new research group.
    /// </summary>
    /// <param name="createResearchGroupDto">Data for creating a new research group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpPost("create-research-group")]
    public async Task<ActionResult> AddResearchGroup(
        [FromBody] CreateUpdateResearchGroupDto createResearchGroupDto) => HandleOperationResult(
                  await Mediator.Send(new CreateResearchGroupCommand(createResearchGroupDto, User.FindFirstValue(ClaimTypes.NameIdentifier))));

    /// <summary>
    /// Deletes a research group.
    /// </summary>
    /// <param name="researchGroupId">The identifier of the research group to delete.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpDelete("delete-research-group/{researchGroupId}")]
    public async Task<ActionResult> DeleteResearchGroup(Guid researchGroupId) => HandleOperationResult(
                  await Mediator.Send(new DeleteResearchGroupCommand(researchGroupId)));

    /// <summary>
    /// Updates the name of a research group.
    /// </summary>
    /// <param name="researchGroupId">The identifier of the research group to update.</param>
    /// <param name="newGroupName">The new name for the research group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpPut("update-research-group/{researchGroupId}")]
    public async Task<ActionResult> UpdateResearchGroup(Guid researchGroupId,
        [FromBody] CreateUpdateResearchGroupDto updateResearchGroup) => HandleOperationResult(
                  await Mediator.Send(new UpdateResearchGroupCommand(researchGroupId, updateResearchGroup)));

    /// <summary>
    /// Adds a patient to a research group.
    /// </summary>
    /// <param name="researchGroupId">The identifier of the research group.</param>
    /// <param name="patientId">The identifier of the patient to add to the research group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpPost("add-patient/{researchGroupId}")]
    public async Task<ActionResult> AddPatientToResearchGroup(Guid researchGroupId,
        Guid patientId) => HandleOperationResult(
                  await Mediator.Send(new AddPatientToResearchGroupCommand(researchGroupId, patientId)));

    /// <summary>
    /// Removes a patient from any research group.
    /// </summary>
    /// <param name="patientId">The identifier of the patient to remove from any research group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpDelete("remove-patient/{patientId}")]
    public async Task<ActionResult> RemovePatientFromResearchGroup(Guid patientId) => HandleOperationResult(
              await Mediator.Send(new RemovePatientFromResearchGroupCommand(patientId)));

    /// <summary>
    /// Retrieves details about a research group by research group id.
    /// </summary>
    /// <param name="researchGroupId">The identifier of the research group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [Authorize(Roles = "Dentist_Teacher_Researcher")]
    [HttpGet("research-group-details/{researchGroupId}")]
    public async Task<ActionResult> GetResearchGroupDetailsById(Guid researchGroupId) => HandleOperationResult(
                     await Mediator.Send(new FetchResearchGroupDetailsByIdQuery(researchGroupId)));

}
