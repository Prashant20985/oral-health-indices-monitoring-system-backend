using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Models.Logs;

/// <summary>
/// Represents the document model for a request log.
/// </summary>
[BsonIgnoreExtraElements]
public class RequestLogDocument
{
    /// <summary>
    /// Gets or sets the Id of the request log.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the request log.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the log level of the request log.
    /// </summary>
    public string Level { get; set; }

    /// <summary>
    /// Gets or sets the message template of the request log.
    /// </summary>
    public string MessageTemplate { get; set; }

    /// <summary>
    /// Gets or sets the rendered message of the request log.
    /// </summary>
    public string RenderedMessage { get; set; }

    /// <summary>
    /// Gets or sets the RequestLogProperties of the request log.
    /// </summary>
    public RequestLogProperties Properties { get; set; }
}
