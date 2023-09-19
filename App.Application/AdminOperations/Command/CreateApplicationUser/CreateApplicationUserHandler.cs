using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.CreateApplicationUser;

/// <summary>
/// Represents a handler for the CreateApplicationUserCommand.
/// </summary>
internal sealed class CreateApplicationUserHandler
    : IRequestHandler<CreateApplicationUserCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IGeneratePassword _generatePassword;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateApplicationUserHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for managing user-related operations.</param>
    /// <param name="generatePassword">The henerate password instance.</param>
    /// <param name="mediator">The mediator for handling communication between application components.</param>
    public CreateApplicationUserHandler(IUserRepository userRepository,
        IGeneratePassword generatePassword,
        IMediator mediator)
    {
        _userRepository = userRepository;
        _generatePassword = generatePassword;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the command to create an application user.
    /// </summary>
    /// <param name="request">The command containing the user's information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        // Check if a user with the given email or username already exists.
        var userExists = await _userRepository.GetUserByUserNameOrEmail(request.CreateApplicationUser.Email, cancellationToken);

        if (userExists is not null)
            return OperationResult<Unit>.Failure("Email already taken");

        // Create an ApplicationUser instance with provided user information.
        ApplicationUser applicationUser = new(
            email: request.CreateApplicationUser.Email,
            firstName: request.CreateApplicationUser.FirstName,
            lastName: CheckNullOrWhiteSpace(request.CreateApplicationUser.LastName),
            phoneNumber: CheckNullOrWhiteSpace(request.CreateApplicationUser.PhoneNumber),
            guestUserComment: CheckNullOrWhiteSpace(request.CreateApplicationUser.GuestUserComment));

        // Generate a random password for the new user.
        var password = _generatePassword.GenerateRandomPassword();

        // Attempt to create the application user in the repository.
        var createUserResult = await _userRepository.CreateApplicationUserAsync(applicationUser, password);

        if (!createUserResult.Succeeded)
            return OperationResult<Unit>.Failure($"{applicationUser.FirstName} {applicationUser.LastName}: " +
                $"{string.Join("\n", createUserResult.Errors)}");

        // Add the newly created user to the role.
        await _userRepository.AddApplicationUserToRoleAsync(applicationUser, request.CreateApplicationUser.Role);

        // Prepare email content for account registration notification.
        var emailContent = new EmailContentDto(
            receiverEmail: applicationUser.Email,
            subject: "Account Created",
            message: $"{applicationUser.UserName} {password}",
            emailType: EmailType.Registration);

        // Publish an email notification for account registration.
        await _mediator.Publish(new EmailNotification(emailContent), cancellationToken);

        return OperationResult<Unit>.Success(Unit.Value);
    }

    // Helper method to check for null or whitespace and return null if the value is empty.
    private static string CheckNullOrWhiteSpace(string value) => string.IsNullOrWhiteSpace(value) ? null : value;
}
