using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;
using Org.BouncyCastle.Bcpg;

namespace App.Application.UserRequestOperations.Query.RequestsListByUserId;

/// <summary>
/// Handler for fetching a list of user requests by user ID.
/// </summary>
internal sealed class FetchRequestsListByUserIdHandler
    : IRequestHandler<FetchRequestsListByUserIdQuery, OperationResult<List<UserRequestDto>>>
{
    private readonly IUserRequestRepository _userRequestRepository;
    
    /// <summary>
    /// Initializes a new instance of the FetchRequestsListByUserIdHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user requests.</param>
    public FetchRequestsListByUserIdHandler(IUserRequestRepository userRequestRepository) =>
        _userRequestRepository = userRequestRepository;
    
    /// <summary>
    /// Handles the retrieval of a list of user requests by user ID.
    /// </summary>
    /// <param name="request">The query to fetch user requests by user ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result containing a list of user request DTOs or a failure message.</returns>
    public async Task<OperationResult<List<UserRequestDto>>> Handle
        (FetchRequestsListByUserIdQuery request, CancellationToken cancellationToken)
    {
        var requestsByUserIdQuery = await _userRequestRepository.GetRequestsByUserId(request.UserId);
        
        if (requestsByUserIdQuery is null)
            return OperationResult<List<UserRequestDto>>.Failure("No requests found by given userId");

        return OperationResult<List<UserRequestDto>>.Success(requestsByUserIdQuery);
    }
}