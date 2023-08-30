using App.Application.Core;
using App.Domain.DTOs;

namespace App.Application.Interfaces;

/// <summary>
/// Service class to apply filters on IQueryable instances.
/// </summary>
public interface IQueryFilter
{
    Task<List<ApplicationUserDto>> ApplyFilters(
        IQueryable<ApplicationUserDto> query,
        SearchParams pagingAndSearchParams,
        CancellationToken cancellationToken);
}
