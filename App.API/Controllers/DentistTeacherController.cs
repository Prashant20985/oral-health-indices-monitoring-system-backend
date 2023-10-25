using App.Application.DentistTeacherOperations.Command.CreateGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

[Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
public class DentistTeacherController : BaseController
{
    [HttpPost("create-group")]
    public async Task<ActionResult> CreateGroup(string GroupName) => HandleOperationResult(
        await Mediator.Send(new CreateGroupCommand(GroupName, User.FindFirstValue(ClaimTypes.NameIdentifier))));
}
