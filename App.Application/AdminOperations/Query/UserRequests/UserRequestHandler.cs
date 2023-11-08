using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.AdminOperations.Query.UserRequests;

/// <summary>
/// Handler for processing UserRequestQuery requests.
/// </summary>
internal sealed class UserRequestHandler
    : IRequestHandler<UserRequestQuery, OperationResult<List<UserRequestDto>>>
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
    public async Task<OperationResult<List<UserRequestDto>>> Handle(UserRequestQuery request, CancellationToken cancellationToken)
    {
        var status = Enum.Parse<RequestStatus>(request.RequestStatus);
        var result = await _userRequestRepository
            .GetAllRequestsByStatusAndDateSubmitted(status, request.DateSubmitted);

        if (result.IsNullOrEmpty())
            return OperationResult<List<UserRequestDto>>.Failure("No Requests Found");

        return OperationResult<List<UserRequestDto>>.Success(result);
    }
}
