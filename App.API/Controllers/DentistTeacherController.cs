using App.Application.DentistTeacherOperations.Command.AddStudentToGroup;
using App.Application.DentistTeacherOperations.Command.CreateGroup;
using App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}
