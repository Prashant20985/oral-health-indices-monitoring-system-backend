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
public class LogController(LogService logService) : ControllerBase
{
    // Represents the LogService instance for accessing the logs.
    private readonly LogService _logService = logService;

    /// <summary>
    /// Gets the logs for today.
    /// </summary>
    /// <returns>Returns a list of request logs for today.</returns>
    [HttpGet("today")]
    public async Task<ActionResult<List<RequestLogDocument>>> GetLogsForTodayAsync() =>
        await _logService.GetLogsForTodayAsync();


    /// <summary>
    /// Gets the filtered logs.
    /// </summary>
    /// <param name="query">The LogQueryParameters instance for filtering the logs.</param>
    /// <returns>Returns a list of filtered request logs.</returns>
    [HttpGet("filtered-logs")]
    public async Task<ActionResult<List<RequestLogDocument>>> GetFilteredLogs([FromQuery] LogQueryParameters query) =>
        await _logService.GetFilteredLogs(query);
}
