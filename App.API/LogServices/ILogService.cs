using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Logs;

namespace App.API.LogServices;
/// <summary>
///  Represents the service for managing logs.
/// </summary>
public interface ILogService
{
    /// <summary>
    ///  Retrieves a list of logs.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<LogResponseDto> GetFilteredLogs(LogQueryParameters query);
    /// <summary>
    /// Retrieves a list of logs for today.
    /// </summary>
    /// <returns></returns>
    Task<List<RequestLogDocument>> GetLogsForTodayAsync();
}
