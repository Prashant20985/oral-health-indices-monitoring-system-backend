using App.Domain.Models.Logs;
using Microsoft.AspNetCore.Mvc;

namespace App.API.LogServices;

public interface ILogService
{
    Task<List<RequestLogDocument>> GetFilteredLogs(LogQueryParameters query);
    Task<List<RequestLogDocument>> GetLogsForTodayAsync();
}
