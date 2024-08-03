using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;

namespace App.Application.AdminOperations.Query.DeletedApplicationUsersList;

/// <summary>
/// Represents a request to fetch a paged list of deleted application users.
/// </summary>
public record FetchDeletedApplicationUsersListQuery(
    ApplicationUserPaginationAndSearchParams Params)
        : IRequest<OperationResult<PaginatedApplicationUserResponseDto>>;
