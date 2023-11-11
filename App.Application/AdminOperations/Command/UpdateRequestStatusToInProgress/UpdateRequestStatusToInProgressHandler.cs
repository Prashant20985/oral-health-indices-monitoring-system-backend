using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateRequestStatusToInProgress;
/// <summary>
/// Handler for updating the status of a user request to "In Progress".
/// </summary>
internal sealed class UpdateRequestStatusToInProgressHandler
: IRequestHandler<UpdateRequestStatusToInProgressCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateRequestStatusToInProgressHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user requests.</param>
    public UpdateRequestStatusToInProgressHandler(IUserRequestRepository userRequestRepository)
    {
        _userRequestRepository = userRequestRepository;
    }
    
    /// <summary>
    /// Handles the update of a user request status to "In Progress".
    /// </summary>
    /// <param name="requestStatus">The command to update the user request status.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateRequestStatusToInProgressCommand requestStatus, CancellationToken cancellationToken)
    {
        var userRequest = await _userRequestRepository.GetUserRequestById(requestStatus.UserRequestId);
        
        if (userRequest is null)
            return OperationResult<Unit>.Failure("User request not found");
    
        userRequest.SetRequestToInProgress();
 
        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}