using App.Application.DentistTeacherOperations.Command.AddStudentToGroup;
using App.Application.DentistTeacherOperations.Command.CreateGroup;
using App.Application.DentistTeacherOperations.Command.DeleteGroup;
using App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;
using App.Application.DentistTeacherOperations.Command.UpdateGroupName;
using App.Application.DentistTeacherOperations.Query.Groups;
using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace App.API.Controllers;

[Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
public class DentistTeacherController : BaseController
{
    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="GroupName">The name of the group to create.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpPost("create-group")]
    public async Task<ActionResult> CreateGroup(string GroupName) => HandleOperationResult(
        await Mediator.Send(new CreateGroupCommand(GroupName, User.FindFirstValue(ClaimTypes.NameIdentifier))));


    /// <summary>
    /// Adds a student to a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group to which the student will be added.</param>
    /// <param name="studentId">The identifier of the student to add to the group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpPost("add-student")]
    public async Task<ActionResult> AddStudentToGroup(Guid groupId, string studentId) => HandleOperationResult(
        await Mediator.Send(new AddStudentToGroupCommand(groupId, studentId)));


    /// <summary>
    /// Removes a student from a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group from which the student will be removed.</param>
    /// <param name="studentId">The identifier of the student to remove from the group.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpDelete("remove-student")]
    public async Task<ActionResult> RemoveStudentFromGroup(Guid groupId, string studentId) => HandleOperationResult(
    await Mediator.Send(new RemoveStudentFromGroupCommand(groupId, studentId)));

    /// <summary>
    /// Deletes group
    /// </summary>
    /// <param name="groupId">The identifier of the group which will be deleted.</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpDelete("delete-group")]
    public async Task<ActionResult> DeleteGroup(Guid groupId) => HandleOperationResult(
        await Mediator.Send(new DeleteGroupCommand(groupId)));

    /// <summary>
    /// Updates name of group
    /// </summary>
    /// <param name="groupId">The identifier of the group whch groups name will be updated</param>
    /// <param name="groupName"></param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpPut("update-groupname")]
    public async Task<IActionResult> UpdateGroupName(Guid groupId, string groupName) => HandleOperationResult(
        await Mediator.Send(new UpdateGroupNameCommand(groupId, groupName)));

    /// <summary>
    /// fetches students not in the group
    /// </summary>
    /// <param name="groupId">The identiffier of the group in which students are not present</param>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpGet("get-studentsNotInGroup")]
    public async Task<IActionResult> GetStudentsNotInGroup([Required] Guid groupId) => HandleOperationResult(
        await Mediator.Send(new FetchStudentsNotInGroupListQuery(groupId)));

    /// <summary>
    /// Retrieves a list of all groups associated with the currently authentcated teacher. 
    /// </summary>
    /// <returns>An HTTP response indicating the result of the operation.</returns>
    [HttpGet("groups")]
    public async Task<ActionResult<List<GroupDto>>> GetAllGroups() => HandleOperationResult(
        await Mediator.Send(new FetchGroupsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier))));
}
