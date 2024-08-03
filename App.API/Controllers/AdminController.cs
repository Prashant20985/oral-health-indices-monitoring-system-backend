using App.Application.AdminOperations.Command.ChangeActivationStatus;
using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;
using App.Application.AdminOperations.Command.DeleteApplicationUser;
using App.Application.AdminOperations.Command.UpdateApplicationUser;
using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;
using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Application.AdminOperations.Query.UserDetails;
using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.API.Controllers;

public class AdminController : BaseController
{
    /// <summary>
    /// Gets a paged list of active users.
    /// </summary>
    /// <param name="pagingAndSearchParams">Search and paging params to filter and page user list</param>
    /// <returns>A paged list of active users.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("active-users")]
    public async Task<ActionResult<PaginatedApplicationUserResponseDto>> GetActiveUsers(
        [FromQuery] ApplicationUserPaginationAndSearchParams pagingAndSearchParams) => HandleOperationResult(
            await Mediator.Send(new FetchActiveApplicationUsersListQuery(pagingAndSearchParams)));


    /// <summary>
    /// Gets a paged list of deactivated users.
    /// </summary>
    /// <param name="pagingAndSearchParams">Search and paging params to filter and page user list</param>
    /// <returns>A paged list of deactivated users.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("deactivated-users")]
    public async Task<ActionResult<PaginatedApplicationUserResponseDto>> GetDeactivatedUsers(
        [FromQuery] ApplicationUserPaginationAndSearchParams pagingAndSearchParams) => HandleOperationResult(
            await Mediator.Send(new FetchDeactivatedApplicationUsersListQuery(pagingAndSearchParams)));


    /// <summary>
    /// Gets a paged list of deleted users.
    /// </summary>
    /// <param name="pagingAndSearchParams">Search and paging params to filter and page user list</param>
    /// <returns>A paged list of deleted users.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("deleted-users")]
    public async Task<ActionResult<PaginatedApplicationUserResponseDto>> GetDeletedUsers(
        [FromQuery] ApplicationUserPaginationAndSearchParams pagingAndSearchParams) => HandleOperationResult(
            await Mediator.Send(new FetchDeletedApplicationUsersListQuery(pagingAndSearchParams)));


    /// <summary>
    /// Gets details of a user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>Details of the specified user.</returns>
    [Authorize]
    [HttpGet("user-details/{userName}")]
    public async Task<ActionResult<ApplicationUserResponseDto>> GetUserDetails(string userName) => HandleOperationResult(
            await Mediator.Send(new FetchUserDetailsQuery(userName)));


    /// <summary>
    /// Changes activation status of specified user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>The result from activation status change operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("change-activation-status/{userName}")]
    public async Task<ActionResult> ChangeActivationStatus(string userName) => HandleOperationResult(
            await Mediator.Send(new ChangeActivationStatusCommand(userName)));


    /// <summary>
    /// Deletes specified user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>The result from delete operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-user/{userName}")]
    public async Task<ActionResult> DeleteUser(string userName,
        [Required] string deleteComment) => HandleOperationResult(
                await Mediator.Send(new DeleteApplicationUserCommand(userName, deleteComment)));


    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="createApplicationUser">Data to create a new user.</param>
    /// <returns>The result of the user creation operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateApplicationUserRequestDto createApplicationUser) => HandleOperationResult(
            await Mediator.Send(new CreateApplicationUserCommand(createApplicationUser)));


    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="userName">The username of the user to update.</param>
    /// <param name="updateApplicationUser">Updated user data.</param>
    /// <returns>The result of the user update operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("update-user/{userName}")]
    public async Task<ActionResult> UpdateUser(string userName,
        UpdateApplicationUserRequestDto updateApplicationUser) => HandleOperationResult(
            await Mediator.Send(new UpdateApplicationUserCommand(userName, updateApplicationUser)));


    /// <summary>
    /// Creates a new users from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing users data.</param>
    /// <returns>The result of the user creation operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("create-users")]
    public async Task<ActionResult> CreateUsersFromCsv(IFormFile file) => HandleOperationResult(
        await Mediator.Send(new CreateApplicationUsersFromCsvCommand(file)));

}
