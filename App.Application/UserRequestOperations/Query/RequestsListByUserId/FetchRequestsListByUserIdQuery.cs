using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.UserRequestOperations.Query.RequestsListByUserId;

/// <summary>
/// Represents a query to fetch a list of user requests by user ID.
/// </summary>
public record FetchRequestsListByUserIdQuery(
        string UserId, string RequestStatus, DateTime? DateSubmitted)
    : IRequest<OperationResult<List<UserRequestDto>>>;