using System.Security.Claims;
using App.Application.UserRequestOperations.Command.CreateRequest;
using App.Application.UserRequestOperations.Command.DeleteRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;
/// <summary>
/// Controller for managing user requests.
/// </summary>
[Authorize]
public class UserRequestController:BaseController
{
    /// <summary>
    /// Creates a new user request.
    /// </summary>
    /// <param name="requestTitle">The title of the request.</param>
    /// <param name="description">The description of the request.</param>
    /// <returns>An action result with the result of the request creation operation.</returns>
    [HttpPost("create-request")]
    public async Task<ActionResult> CreateRequest(
        string requestTitle, string description) => HandleOperationResult(
        await Mediator.Send(new CreateRequestCommand(requestTitle, description,
            User.FindFirstValue(ClaimTypes.NameIdentifier))));
    /// <summary>
    /// Deletes a user request.
    /// </summary>
    /// <param name="userRequestId">The unique identifier of the user request to delete.</param>
    /// <returns>An action result with the result of the request deletion operation.</returns>
    [HttpDelete("delete-request/{userRequestId}")]
    public async Task<ActionResult> DeleteRequest(Guid userRequestId) => HandleOperationResult(
            await Mediator.Send(new DeleteRequestCommand(userRequestId)));
}