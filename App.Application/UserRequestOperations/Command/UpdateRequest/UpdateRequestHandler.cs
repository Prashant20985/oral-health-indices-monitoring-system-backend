using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.UserRequestOperations.Command.UpdateRequest;

/// <summary>
/// Handler for processing the UpdateRequestCommand.
/// </summary>
internal sealed class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateRequestHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user request data.</param>
    public UpdateRequestHandler(IUserRequestRepository userRequestRepository) =>
        _userRequestRepository = userRequestRepository;

    /// <summary>
    /// Handles the UpdateRequestCommand to update a user request's title and description.
    /// </summary>
    /// <param name="request">The UpdateRequestCommand instance containing request details.</param>
    /// <param name="cancellationToken">A token for cancellation.</param>
    /// <returns>An OperationResult indicating the success of the update operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        var userRequest = await _userRequestRepository.GetUserRequestById(request.RequestId);

        if (userRequest is null)
            return OperationResult<Unit>.Failure("User request not found");

        userRequest.UpdateRequestTitleAndDescription(request.Title, request.Description);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
