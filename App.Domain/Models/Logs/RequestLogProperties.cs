using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Models.Logs;

/// <summary>
/// Represents the properties of a request log.
/// </summary>
[BsonIgnoreExtraElements]
public class RequestLogProperties
{
    /// <summary>
    /// Gets or sets the executed by property.
    /// </summary>
    public string ExecutedBy { get; set; }

    /// <summary>
    /// Gets or sets the request name property.
    /// </summary>
    public string RequestName { get; set; }

    /// <summary>
    /// Gets or sets the DateTimeUtc property.
    /// </summary>
    public string DateTimeUtc { get; set; }
}
