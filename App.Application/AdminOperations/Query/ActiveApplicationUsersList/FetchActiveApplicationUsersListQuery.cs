using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;

namespace App.Application.AdminOperations.Query.ActiveApplicationUsersList;

/// <summary>
/// Represents a request to fetch a paged list of active application users.
/// </summary>
public record FetchActiveApplicationUsersListQuery(
    SearchParams Params)
        : IRequest<OperationResult<List<ApplicationUserResponseDto>>>;

