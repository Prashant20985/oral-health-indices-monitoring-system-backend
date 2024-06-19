namespace App.Domain.Models.Logs;

/// <summary>
/// Represents the database settings for logs.
/// </summary>
public class LogsDatabaseSettings
{
    /// <summary>
    /// Gets or sets the connection string for the logs database.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// Gets or sets the database name for the logs database.
    /// </summary>
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection name for the logs database.
    /// </summary>
    public string CollectionName { get; set; } = null!;
}
