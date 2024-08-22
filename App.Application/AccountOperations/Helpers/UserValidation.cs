using App.Application.Core;
using App.Domain.Models.Users;

namespace App.Application.AccountOperations.Helpers;
/// <summary>
/// The UserValidation helper class provides methods to validate user information.
/// </summary>
internal static class UserValidation
{
    /// <summary>
    /// Checks the validity of a user.
    /// </summary>
    /// <typeparam name="T">The type of result.</typeparam>
    /// <param name="user">The user to check.</param>
    /// <returns>A result indicating the user's validity.</returns>
    public static OperationResult<T> CheckUserValidity<T>(ApplicationUser user)
    {
        // Return failure if the user is null
        if (user is null)
            return OperationResult<T>.Failure("Invalid Email or Username");

        // Return unauthorized if the user account has been deleted
        if (user.DeletedAt is not null)
            return OperationResult<T>.Unauthorized("Your Account has been deleted");

        // Return unauthorized if the user account is deactivated
        if (!user.IsAccountActive)
            return OperationResult<T>.Unauthorized("Your Account has been Deactivated");

        // Return null if all conditions pass, indicating the user is valid
        return null; 
    }
}
