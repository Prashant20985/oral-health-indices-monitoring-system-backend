namespace App.Application.Core;

/// <summary>
/// Represents paging and search parameters for queries.
/// </summary>
public class SearchParams
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
}
