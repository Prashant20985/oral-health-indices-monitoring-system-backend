using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;

namespace App.Application.Interfaces;

/// <summary>
/// Service class to apply filters on IQueryable instances.
/// </summary>
public interface IQueryFilter
{
    Task<List<ApplicationUserResponseDto>> ApplyFilters(
        IQueryable<ApplicationUserResponseDto> query,
        SearchParams pagingAndSearchParams,
        CancellationToken cancellationToken);
}
