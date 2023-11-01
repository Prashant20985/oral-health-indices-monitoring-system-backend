using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.UserRequestOperations.Command.DeleteRequest;

/// <summary>
/// Handler for deleting a user request.
/// </summary>
internal sealed class DeleteRequestHandler
    : IRequestHandler<DeleteRequestCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;
    
    /// <summary>
    /// Initializes a new instance of the DeleteRequestHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user requests.</param>
    public DeleteRequestHandler(IUserRequestRepository userRequestRepository) =>
        _userRequestRepository = userRequestRepository;
   
    /// <summary>
    /// Handles the deletion of a user request.
    /// </summary>
    /// <param name="request">The command to delete a user request.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        var userRequest = await _userRequestRepository.GetUserRequestById(request.UserRequestId);

        if (userRequest is null)
            return OperationResult<Unit>.Failure("Request not found");
        
        _userRequestRepository.DeleteRequest(userRequest);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}