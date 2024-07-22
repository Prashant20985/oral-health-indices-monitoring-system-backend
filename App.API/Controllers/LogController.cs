using App.API.LogServices;
using App.Domain.Models.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;


/// <summary>
/// Represents the controller for logs.
/// </summary>
/// <param name="logService">The LogService instance for accessing the logs.</param>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class LogController(ILogService logService) : ControllerBase
{
    // Represents the LogService instance for accessing the logs.
    private readonly ILogService _logService = logService;

    /// <summary>
    /// Gets the logs for today.
    /// </summary>
    /// <returns>Returns a list of request logs for today.</returns>
    [HttpGet("today")]
    public async Task<ActionResult<List<RequestLogDocument>>> GetLogsForTodayAsync()
    {
            var logs = await _logService.GetLogsForTodayAsync();
            return Ok(logs);
    }


    /// <summary>
    /// Gets the filtered logs.
    /// </summary>
    /// <param name="query">The LogQueryParameters instance for filtering the logs.</param>
    /// <returns>Returns a list of filtered request logs.</returns>
    [HttpGet("filtered-logs")]
    public async Task<ActionResult> GetFilteredLogs([FromQuery] LogQueryParameters query) =>
        Ok(await _logService.GetFilteredLogs(query));
}
