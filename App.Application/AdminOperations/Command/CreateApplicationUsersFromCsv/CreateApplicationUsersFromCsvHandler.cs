using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;

/// <summary>
/// Represents a handler for creating application users from a CSV file.
/// </summary>
internal sealed class CreateApplicationUsersFromCsvHandler
    : IRequestHandler<CreateApplicationUsersFromCsvCommand,
    OperationResult<string>>
{
    private readonly IMediator _mediator;
    private readonly IReadCsv _readCsv;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateApplicationUsersFromCsvHandler"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for handling communication between application components.</param>
    /// <param name="readCsv">The read csv instance.</param>
    public CreateApplicationUsersFromCsvHandler(IMediator mediator,
        IReadCsv readCsv)
    {
        _mediator = mediator;
        _readCsv = readCsv;
    }

    /// <summary>
    /// Handles the command to create application users from a CSV file.
    /// </summary>
    /// <param name="request">The command containing the CSV file.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<string>> Handle(CreateApplicationUsersFromCsvCommand request, CancellationToken cancellationToken)
    {
        List<CreateApplicationUserDto> applicationUserToCreate;

        try
        {
            // Read user data from the provided CSV file.
            applicationUserToCreate = _readCsv.ReadApplicationUsersFromCsv(request.File);
        }
        catch (Exception ex)
        {
            // If an exception occurs during CSV reading, return a failure result with the error message.
            return OperationResult<string>.Failure(ex.Message);
        }

        if (applicationUserToCreate.Count <= 0)
            return OperationResult<string>.Failure("No users found in the CSV file");

        var failedToCreateUsers = new List<string>();
        var createdUsers = new List<string>();

        // Loop through each application user data read from the CSV file.
        foreach (var applicationUser in applicationUserToCreate)
        {
            // Send a command to create an application user using the mediator.
            var result = await _mediator.Send(new CreateApplicationUserCommand(applicationUser), cancellationToken);

            // Check if the user creation was successful or not.
            if (!result.IsSuccessful)
                failedToCreateUsers.Add($"{applicationUser.FirstName} {applicationUser.LastName}: {result.ErrorMessage} \n");
            else
                createdUsers.Add($"{applicationUser.FirstName} {applicationUser.LastName}: Created Successfully \n");
        }

        // Generate summary messages for successful and failed user creations.
        var successMessage = "Created following users: \n\n" + string.Join("", createdUsers);
        var errorMessage = "Failed to add the following users: \n\n" + string.Join("", failedToCreateUsers);

        // Check if any user creations failed and return appropriate result.
        if (failedToCreateUsers.Count > 0)
        {
            return OperationResult<string>.Success($"{successMessage} \n{errorMessage}");
        }

        return OperationResult<string>.Success($"{successMessage}");
    }
}
