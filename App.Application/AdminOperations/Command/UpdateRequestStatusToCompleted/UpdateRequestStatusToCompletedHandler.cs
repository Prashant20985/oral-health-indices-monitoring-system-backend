using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateRequestStatusToCompleted;
/// <summary>
/// Handler for updating the status of a user request to "Completed".
/// </summary>
internal sealed class UpdateRequestStatusToCompletedHandler
: IRequestHandler<UpdateRequestStatusToCompletedCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateRequestStatusToCompletedHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user requests.</param>
    public UpdateRequestStatusToCompletedHandler(IUserRequestRepository userRequestRepository)
    {
        _userRequestRepository = userRequestRepository;
    }
    
    /// <summary>
    /// Handles the update of a user request status to "Completed".
    /// </summary>
    /// <param name="requestStatus">The command to update the user request status.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateRequestStatusToCompletedCommand requestStatus, CancellationToken cancellationToken)
    {
        var userRequest = await _userRequestRepository.GetUserRequestById(requestStatus.UserRequestId);
        
        if (userRequest is null)
            return OperationResult<Unit>.Failure("User request not found");
    
        userRequest.SetRequestToCompleted(requestStatus.AdminComment);

 
        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
    
}