using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;

/// <summary>
/// Represents a request to fetch a paged list of deactivated application users.
/// </summary>
public record FetchDeactivatedApplicationUsersListQuery(
    PagingAndSearchParams Params)
        : IRequest<OperationResult<PagedList<ApplicationUserDto>>>;
