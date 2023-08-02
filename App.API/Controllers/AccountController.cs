using App.Application.AccountOperations.ChangePassword;
using App.Application.AccountOperations.CurrentUser;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Login;
using App.Application.AccountOperations.RefreshToken;
using App.Application.AccountOperations.ResetPassword;
using App.Application.AccountOperations.ResetPasswordUrlEmailRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
            HandleOperationResult(await Mediator.Send(new LoginCommand(credentials)));


        /// <summary>
        /// Changes the password for the authenticated user.
        /// </summary>
        /// <param name="changePassword">The DTO object containing the new password details.</param>
        /// <returns>An action result indicating the success of the password change operation.</returns>
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword) =>
            HandleOperationResult(await Mediator.Send(new ChangePasswordCommand(changePassword)));


        /// <summary>
        /// Gets the current authenticated user.
        /// </summary> 
        /// <returns>The action result containing the user information.</returns>
        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserLoginResponseDto>> GetCurrentUser() =>
            HandleOperationResult(await Mediator.Send(new CurrentUserQuery(User.FindFirstValue(ClaimTypes.Name))));


        /// <summary>
        /// Sends a password reset email notification to the specified email address.
        /// </summary>
        /// <param name="email">The email address of the user requesting the password reset.</param>
        /// <returns>An action result indicating the success of the password reset email request</returns>
        [AllowAnonymous]
        [HttpPost("reset-password/{email}")]
        public async Task<ActionResult> ResetPasswordEmailNotification([Required] string email) =>
            HandleOperationResult(await Mediator.Send(new ResetPasswordUrlEmailRequestCommand(email)));

        /// <summary>
        /// Displays the password reset page for the user with the provided token and email address.
        /// </summary>
        /// <param name="token">The password reset token.</param>
        /// <param name="email">The email address of the user.</param>
        /// <returns>An action result displaying the password reset page.</returns>
        [AllowAnonymous]
        [HttpGet("reset-password")]
        public ActionResult ResetPassword(string token, string email) =>
            Ok(new ResetPasswordDto { Token = token, Email = email });


        /// <summary>
        /// Resets the password for the user with the provided password reset details.
        /// </summary>
        /// <param name="resetPassword">The DTO object containing the password reset details.</param>
        /// <returns>An action result indicating the success of the password reset operation.</returns>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword) =>
            HandleOperationResult(await Mediator.Send(new ResetPasswordCommand(resetPassword)));


        /// <summary>
        /// Refreshes the authentication token for the authenticated user.
        /// </summary>
        /// <returns>An action result representing the outcome of the token refresh operation.</returns>
        [Authorize]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserLoginResponseDto>> RefreshToken() =>
            HandleOperationResult(await Mediator.Send(new RefreshTokenCommand(User.FindFirstValue(ClaimTypes.Name))));
    }
}
