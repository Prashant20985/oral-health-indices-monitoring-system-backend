using App.Application.Core;
using App.Domain.DTOs.UserRequestDtos.Response;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.AdminOperations.Query.UserRequests;

/// <summary>
/// Handler for processing UserRequestQuery requests.
/// </summary>
internal sealed class UserRequestHandler
    : IRequestHandler<UserRequestQuery, OperationResult<List<UserRequestResponseDto>>>
{
    private readonly IUserRequestRepository _userRequestRepository;

    /// <summary>
    /// Initializes a new instance of the UserRequestHandler class.
    /// </summary>
    /// <param name="userRequestRepository">The repository for user request data.</param>
    public UserRequestHandler(IUserRequestRepository userRequestRepository) =>
        _userRequestRepository = userRequestRepository;

    /// <summary>
    /// Handles the UserRequestQuery request and retrieves user requests based on their status.
    /// </summary>
    /// <param name="request">The UserRequestQuery instance containing the request status.</param>
    /// <param name="cancellationToken">A token for cancellation.</param>
    /// <returns>An OperationResult containing a list of user requests with the specified status.</returns>
    public async Task<OperationResult<List<UserRequestResponseDto>>> Handle(UserRequestQuery request, CancellationToken cancellationToken)
    {
        // Parse the request status from the query string.
        var status = Enum.Parse<RequestStatus>(request.RequestStatus);
        // Retrieve all user requests with the specified status
        var userRequestsQuery = _userRequestRepository
            .GetAllRequestsByStatus(status);

        // Apply date filter if provided
        if (request.DateSubmitted is not null)
            userRequestsQuery = userRequestsQuery
                .Where(x => x.DateSubmitted.Date == request.DateSubmitted.Value.Date);

        // Execute the query and return the result
        return OperationResult<List<UserRequestResponseDto>>.Success(await userRequestsQuery.ToListAsync());
    }
}
