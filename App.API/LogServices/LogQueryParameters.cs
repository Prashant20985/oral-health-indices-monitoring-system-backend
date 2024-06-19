namespace App.API.LogServices;

/// <summary>
/// Represents the query parameters for filtering logs.
/// </summary>
public class LogQueryParameters
{
    /// <summary>
    /// Gets or sets the start date for the log query.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for the log query.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the user name for the log query.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the log level for the log query.
    /// </summary>
    public string Level { get; set; }
}

