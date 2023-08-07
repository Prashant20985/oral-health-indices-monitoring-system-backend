using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;

/// <summary>
/// Represents a request to fetch a paged list of deactivated application users.
/// </summary>
public record FetchDeactivatedApplicationUsersListQuery(string SearchTerm,
    int PageNumber,
    int PageSize) : IRequest<OperationResult<PagedList<ApplicationUserDto>>>;
