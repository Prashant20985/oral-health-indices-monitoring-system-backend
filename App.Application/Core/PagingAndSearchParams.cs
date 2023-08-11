namespace App.Application.Core;

/// <summary>
/// Represents paging and search parameters for queries.
/// </summary>
public class PagingAndSearchParams
{
    /// <summary>
    /// Gets or sets the search term for filtering results.
    /// </summary>
    public string SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the role for filtering results by role.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Gets or sets the user type for filtering results by user type.
    /// </summary>
    public string UserType { get; set; }

    private const int MaxPageSize = 50;

    /// <summary>
    /// Gets or sets the page number for paging results.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    /// <summary>
    /// Gets or sets the page size for paging results.
    /// It automatically limits the maximum page size to the value of MaxPageSize.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
