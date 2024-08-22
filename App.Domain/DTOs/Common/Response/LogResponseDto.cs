using App.Domain.Models.Logs;

namespace App.Domain.DTOs.Common.Response;

/// <summary>
///  Represents the log response DTO.
/// </summary>
public class LogResponseDto
{
    /// <summary>
    ///   Gets or sets the logs.
    /// </summary>
    public List<RequestLogDocument> Logs { get; set; }
    
    /// <summary>
    ///    Gets or sets the total count.
    ///  </summary>
    public long TotalCount { get; set; }
}
