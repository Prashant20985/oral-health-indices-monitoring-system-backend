using App.Domain.DTOs.ApplicationUserDtos.Response;
using Microsoft.EntityFrameworkCore;

namespace App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;

public class ApplicationUsersListQueryFilter : IApplicationUsersListQuesyFilter
{
    /// <summary>
    /// Apply filters to the given query based on search parameters and return a paged result.
    /// </summary>
    /// <param name="query">The IQueryable of ApplicationUserDto to be filtered.</param>
    /// <param name="pagingAndSearchParams">Paging and search parameters to apply.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A paged list of ApplicationUserDto that match the applied filters.</returns>
    public async Task<PaginatedApplicationUserResponseDto> ApplyFilters(IQueryable<ApplicationUserResponseDto> query,
        ApplicationUserPaginationAndSearchParams pagingAndSearchParams,
        CancellationToken cancellationToken)
    {
        // Apply search term filter if provided
        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.SearchTerm))
            query = query.Where(x =>
                x.UserName.Contains(pagingAndSearchParams.SearchTerm) ||
                x.FirstName.Contains(pagingAndSearchParams.SearchTerm));

        // Apply role filter if provided
        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.Role))
            query = query.Where(x =>
                x.Role == pagingAndSearchParams.Role);

        // Apply user type filter if provided
        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.UserType))
            query = query.Where(x =>
                x.UserType == pagingAndSearchParams.UserType);

        // Get the total count of users after applying filters
        var totalUsersCount = await query.CountAsync(cancellationToken: cancellationToken);

        // Apply pagination
        query = query.Skip((pagingAndSearchParams.Page) * pagingAndSearchParams.PageSize)
            .Take(pagingAndSearchParams.PageSize);

        // Retrieve the filtered and paginated list of users
        var users = await query.ToListAsync(cancellationToken: cancellationToken);

        // Return the paginated result
        return new PaginatedApplicationUserResponseDto
        {
            TotalUsersCount = totalUsersCount,
            Users = users
        };
    }
}
