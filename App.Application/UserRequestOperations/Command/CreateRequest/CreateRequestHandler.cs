using App.Application.Core;
using App.Domain.Models.Users;
using App.Domain.Repository;
using MediatR;

namespace App.Application.UserRequestOperations.Command.CreateRequest;
/// <summary>
/// Handler for creating a user request.
/// </summary>
internal sealed class CreateRequestHandler
: IRequestHandler<CreateRequestCommand, OperationResult<Unit>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the CreateRequestHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user requests.</param>
    public CreateRequestHandler(IUserRequestRepository userRequestRepository) =>
        _userRequestRepository = userRequestRepository;

    /// <summary>
    /// Handles the creation of a user request.
    /// </summary>
    /// <param name="request">The command to create a user request.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result indicating the success of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        // Create a new user request
        UserRequest userRequest = new UserRequest(request.CreatedBy, request.RequestTitle, request.Description);
        
        await _userRequestRepository.CreateRequest(userRequest);

        // Return a success result
        return OperationResult<Unit>.Success(Unit.Value);
    }
}