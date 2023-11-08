using App.Application.Core;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateUserRequest;

/// <summary>
/// Handles the update of request titlw and descrption based on the provided command.
/// </summary>
internal sealed class UpdateUserRequestCommandStatusHandler
    : IRequestHandler<UpdateUserRequestStatusCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateUserRequestCommandStatusHandler class
    /// </summary>
    /// <param name="userRequestRepository">The repository for managing user request-related operations.</param>
    public UpdateUserRequestCommandStatusHandler(IUserRequestRepository userRequestRepository)
    {
        _userRequestRepository = userRequestRepository;
    }

    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(UpdateUserRequestStatusCommand request,
        CancellationToken cancellationToken)
    {
        // Retrive the user request.
        var userRequest = await _userRequestRepository.GetUserRequestById(request.UserRequestId);

        if (userRequest is null)
            return OperationResult<Unit>.Failure("User request not found");

        // Update the request status and admin comment of a user request.
        if (!string.IsNullOrEmpty(request.AdminComment))
            userRequest.AddAdminComment(request.AdminComment);

        userRequest.UpdateRequestStatus(Enum.Parse<RequestStatus>(request.RequestStatus));

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
