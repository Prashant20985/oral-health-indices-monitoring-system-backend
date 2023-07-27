using App.Application.Core;
using App.Domain.Models.Users;

namespace App.Application.AccountOperations.Helpers;

internal static class UserValidation
{
    /// <summary>
    /// Checks the validity of a user.
    /// </summary>
    /// <typeparam name="T">The type of result.</typeparam>
    /// <param name="user">The user to check.</param>
    /// <returns>A result indicating the user's validity.</returns>
    public static OperationResult<T> CheckUserValidity<T>(User user)
    {
        if (user is null)
            return OperationResult<T>.Failure("Invalid Email or Username");

        if (user.DeletedAt is not null)
            return OperationResult<T>.Unauthorized("Your Account has been deleted");

        if (!user.IsAccountActive)
            return OperationResult<T>.Unauthorized("Your Account has been Deactivated");

        return null; // Returns null if all conditions pass
    }
}
