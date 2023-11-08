using App.Application.AdminOperations.Query.UserRequests;
using App.Application.UserRequestOperations.Command.CreateRequest;
using App.Application.UserRequestOperations.Command.DeleteRequest;
using App.Application.UserRequestOperations.Command.UpdateRequest;
using App.Application.UserRequestOperations.Query.RequestsListByUserId;
using App.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

/// <summary>
/// Controller for managing user requests.
/// </summary>
[Authorize]
public class UserRequestController : BaseController
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


    // <summary>
    /// Retrieves a list of user requests by their user ID.
    /// </summary>
    /// <returns>An action result with the result of the request list retrieval operation.</returns>
    [HttpGet("requests-by-userid")]
    public async Task<IActionResult> GetRequestsByUserId(string requestStatus,
        DateTime dateSubmitted) => HandleOperationResult(
        await Mediator.Send(new FetchRequestsListByUserIdQuery(
            User.FindFirstValue(ClaimTypes.NameIdentifier), requestStatus, dateSubmitted)));


    /// <summary>
    /// Retrieves user requests based on their status.
    /// </summary>
    /// <param name="requestStatus">The status of the user requests to retrieve.</param>
    /// <returns>A list of user requests with the specified status.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("user-requests")]
    public async Task<ActionResult<List<UserRequestDto>>>
        GetUserRequestByStatus(string requestStatus, DateTime dateSubmitted) =>
        HandleOperationResult(await Mediator.Send(new UserRequestQuery(requestStatus, dateSubmitted)));


    /// <summary>
    /// Updates the title and description of a user request.
    /// </summary>
    /// <param name="userRequestId">The unique identifier of the user request to update.</param>
    /// <param name="title">The new title for the user request.</param>
    /// <param name="description">The new description for the user request.</param>
    /// <returns>An ActionResult indicating the result of the update operation.</returns>
    [HttpPut("update-request/{userRequestId}")]
    public async Task<ActionResult> UpdateRequestTitleAndDescription(Guid userRequestId,
        string title, string description) => HandleOperationResult(
            await Mediator.Send(new UpdateRequestCommand(userRequestId, title, description)));
}