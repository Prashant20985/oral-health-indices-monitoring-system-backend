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
        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.SearchTerm))
            query = query.Where(x =>
                x.UserName.Contains(pagingAndSearchParams.SearchTerm) ||
                x.FirstName.Contains(pagingAndSearchParams.SearchTerm));

        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.Role))
            query = query.Where(x =>
                x.Role == pagingAndSearchParams.Role);

        if (!string.IsNullOrWhiteSpace(pagingAndSearchParams.UserType))
            query = query.Where(x =>
                x.UserType == pagingAndSearchParams.UserType);

        var totalUsersCount = await query.CountAsync(cancellationToken: cancellationToken);

        query = query.Skip((pagingAndSearchParams.Page) * pagingAndSearchParams.PageSize)
            .Take(pagingAndSearchParams.PageSize);

        var users = await query.ToListAsync(cancellationToken: cancellationToken);

        return new PaginatedApplicationUserResponseDto
        {
            TotalUsersCount = totalUsersCount,
            Users = users
        };
    }
}
