using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Query.UserRequests;

/// <summary>
/// A query record to retrieve user requests based on their status.
/// </summary>
public record UserRequestQuery(string RequestStatus, DateTime? DateSubmitted)
    : IRequest<OperationResult<List<UserRequestDto>>>;
