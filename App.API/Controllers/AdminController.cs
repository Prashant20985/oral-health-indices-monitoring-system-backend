using App.Application.AdminOperations.Command.ChangeActivationStatus;
using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;
using App.Application.AdminOperations.Command.DeleteApplicationUser;
using App.Application.AdminOperations.Command.UpdateApplicationUser;
using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
using App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;
using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Application.AdminOperations.Query.UserDetails;
using App.Application.Core;
using App.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.API.Controllers;

public class AdminController : BaseController
{
    /// <summary>
    /// Gets a paged list of active users.
    /// </summary>
    /// <param name="searchTerm">A search term to filter users.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of active users.</returns>
    //[Authorize(Roles = "Admin")]
    [HttpGet("active-users")]
    public async Task<ActionResult<PagedList<ApplicationUserDto>>> GetActiveUsers(string searchTerm,
        int pageNumber,
        int pageSize) => HandleOperationPagedResult(
            await Mediator.Send(new FetchActiveApplicationUsersPagedListQuery(searchTerm, pageNumber, pageSize)));


    /// <summary>
    /// Gets a paged list of deactivated users.
    /// </summary>
    /// <param name="searchTerm">A search term to filter users.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of deactivated users.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("deactivated-users")]
    public async Task<ActionResult<PagedList<ApplicationUserDto>>> GetDeactivatedUsers(string searchTerm,
        int pageNumber,
        int pageSize) => HandleOperationPagedResult(
            await Mediator.Send(new FetchDeactivatedApplicationUsersListQuery(searchTerm, pageNumber, pageSize)));


    /// <summary>
    /// Gets a paged list of deleted users.
    /// </summary>
    /// <param name="searchTerm">A search term to filter users.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of deleted users.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("deleted-users")]
    public async Task<ActionResult<PagedList<ApplicationUserDto>>> GetDeleetdUsers(string searchTerm,
        int pageNumber,
        int pageSize) => HandleOperationPagedResult(
            await Mediator.Send(new FetchDeletedApplicationUsersListQuery(searchTerm, pageNumber, pageSize)));


    /// <summary>
    /// Gets details of a user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>Details of the specified user.</returns>
    [Authorize]
    [HttpGet("user-details/{userName}")]
    public async Task<ActionResult<ApplicationUserDto>> GetUserDetails(string userName) => HandleOperationResult(
            await Mediator.Send(new FetchUserDetailsQuery(userName)));


    /// <summary>
    /// Changes activation status of specified user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>The result from activation status change operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("chnage-activation-status/{userName}")]
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
        [FromBody] CreateApplicationUserDto createApplicationUser) => HandleOperationResult(
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
        UpdateApplicationUserDto updateApplicationUser) => HandleOperationResult(
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
