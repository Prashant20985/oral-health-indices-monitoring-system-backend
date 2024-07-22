using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Logs;

namespace App.API.LogServices;

public interface ILogService
{
    Task<LogResponseDto> GetFilteredLogs(LogQueryParameters query);
    Task<List<RequestLogDocument>> GetLogsForTodayAsync();
}
