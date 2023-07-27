using App.Application.AccountOperations;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers
{
    /// <summary>
    /// Controller for handling user account-related operations.
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Handles the user login operation.
        /// </summary>
        /// <param name="credentials">The login credentials provided by the user.</param>
        /// <returns>An action result representing the outcome of the login operation.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> UserLogin([FromBody] UserCredentialsDto credentials) =>
            HandleOperationResult(await Mediator.Send(new Login.Command { Credentials = credentials }));


        /// <summary>
        /// Changes the password for the authenticated user.
        /// </summary>
        /// <param name="changePassword">The DTO object containing the new password details.</param>
        /// <returns>An action result indicating the success of the password change operation.</returns>
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword) =>
            HandleOperationResult(await Mediator.Send(new ChangePassword.Command { ChangePassword = changePassword }));


        /// <summary>
        /// Gets the current authenticated user.
        /// </summary>
        /// <returns>The action result containing the user information.</returns>
        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserLoginResponseDto>> GetCurrentUser() =>
            HandleOperationResult(await Mediator.Send(new CurrentUser.Query { UserName = User.FindFirstValue(ClaimTypes.Name) }));

    }
}
