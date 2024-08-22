using App.Application.Core;
using App.Domain.DTOs.UserRequestDtos.Response;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.UserRequestOperations.Query.RequestsListByUserId;

/// <summary>
/// Handler for fetching a list of user requests by user ID.
/// </summary>
internal sealed class FetchRequestsListByUserIdHandler
    : IRequestHandler<FetchRequestsListByUserIdQuery, OperationResult<List<UserRequestResponseDto>>>
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
    public async Task<OperationResult<List<UserRequestResponseDto>>> Handle
        (FetchRequestsListByUserIdQuery request, CancellationToken cancellationToken)
    {
        // Parse the request status string to an enum
        var status = Enum.Parse<RequestStatus>(request.RequestStatus);

        // Get the user requests by user ID and status
        var requestsByUserIdQuery = _userRequestRepository
            .GetRequestsByUserIdAndStatus(request.UserId, status);

        // If the date submitted is not null, filter the requests by date
        if (request.DateSubmitted is not null)
            requestsByUserIdQuery = requestsByUserIdQuery
                .Where(x => x.DateSubmitted.Date == request.DateSubmitted.Value.Date);

        // Return a success result with the list of user request DTOs
        return OperationResult<List<UserRequestResponseDto>>.Success(await requestsByUserIdQuery.ToListAsync());
    }
}