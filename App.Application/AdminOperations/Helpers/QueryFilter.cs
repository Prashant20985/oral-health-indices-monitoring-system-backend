using App.Application.Core;
using App.Domain.DTOs;

namespace App.Application.AdminOperations.Helpers;

/// <summary>
/// Helper class for applying filters to a query of ApplicationUserDto.
/// </summary>
internal static class QueryFilter
{
    /// <summary>
    /// Apply filters to the given query based on search parameters and return a paged result.
    /// </summary>
    /// <param name="query">The IQueryable of ApplicationUserDto to be filtered.</param>
    /// <param name="pagingAndSearchParams">Paging and search parameters to apply.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A paged list of ApplicationUserDto that match the applied filters.</returns>
    internal async static Task<PagedList<ApplicationUserDto>> ApplyFilters(
        IQueryable<ApplicationUserDto> query,
        PagingAndSearchParams pagingAndSearchParams,
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

        var pagedList = await PagedList<ApplicationUserDto>
            .CreateAsync(
                source: query,
                pageNumber: pagingAndSearchParams.PageNumber,
                pageSize: pagingAndSearchParams.PageSize,
                cancellationToken: cancellationToken);

        return pagedList;
    }
}
