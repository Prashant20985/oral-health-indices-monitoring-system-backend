using App.Domain.DTOs.ApplicationUserDtos.Response;

namespace App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;

/// <summary>
/// Service class to apply filters on IQueryable instances.
/// </summary>
public interface IApplicationUsersListQuesyFilter
{
    Task<PaginatedApplicationUserResponseDto> ApplyFilters(
        IQueryable<ApplicationUserResponseDto> query,
        ApplicationUserPaginationAndSearchParams pagingAndSearchParams,
        CancellationToken cancellationToken);
}
