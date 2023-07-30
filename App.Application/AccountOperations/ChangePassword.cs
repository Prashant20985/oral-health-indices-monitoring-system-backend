using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Application.AccountOperations;

public class ChangePassword
{
    public class ChangePasswordCommand : IRequest<OperationResult<Unit>>
    {
        public ChangePasswordDto ChangePassword { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, OperationResult<Unit>>
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        public ChangePasswordHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByNameAsync(request.ChangePassword.Email) ??
                await _userManager.FindByEmailAsync(request.ChangePassword.Email);

            // Check if the user is valid
            var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

            if (userValidityResult is not null)
                return userValidityResult;

            // Check if the new password is the same as the old password
            if (request.ChangePassword.CurrentPassword.Equals(request.ChangePassword.NewPassword))
                return OperationResult<Unit>.Failure("New password cannot be the same as the old password");

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(user, request.ChangePassword.CurrentPassword, request.ChangePassword.NewPassword);

            if (!result.Succeeded)
                return OperationResult<Unit>.Failure(result.Errors.FirstOrDefault().Description.ToString());

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}
