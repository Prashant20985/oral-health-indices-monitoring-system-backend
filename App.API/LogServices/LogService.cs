using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Logs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.API.LogServices;

/// <summary>
/// Represents a service for logging requests.
/// </summary>
public class LogService : ILogService
{
    // Represents the collection of request logs.
    private readonly IMongoCollection<RequestLogDocument> _logCollection;

    /// <summary>
    /// Represents a new instance of the LogService class.
    /// </summary>
    /// <param name="options">The IOptions instance for accessing the logs database settings.</param>
    public LogService(IOptions<LogsDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var database = mongoClient.GetDatabase(options.Value.DatabaseName);
        _logCollection = database.GetCollection<RequestLogDocument>(options.Value.CollectionName);
    }

    /// <summary>
    /// Gets the logs for today.
    /// </summary>
    /// <returns>Returns a list of request logs for today.</returns>
    public async Task<List<RequestLogDocument>> GetLogsForTodayAsync()
    {
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(1).AddTicks(-1);
        var filter = Builders<RequestLogDocument>.Filter.And(
            Builders<RequestLogDocument>.Filter.Gte(log => log.Timestamp, start),
            Builders<RequestLogDocument>.Filter.Lte(log => log.Timestamp, end)
        );
        return await _logCollection.Find(filter).ToListAsync();
    }

    /// <summary>
    /// Gets the filtered logs.
    /// </summary>
    /// <param name="query">The LogQueryParameters instance for filtering the logs.</param>
    /// <returns>Returns a list of filtered request logs.</returns>
    public async Task<LogResponseDto> GetFilteredLogs(LogQueryParameters query)
    {
        // Create a list of filters based on the query parameters.
        var filters = new List<FilterDefinition<RequestLogDocument>>();

        // Add filters based on the query parameters.
        if (query.StartDate.HasValue)
        {
            var start = query.StartDate.Value.Date;
            filters.Add(Builders<RequestLogDocument>.Filter.Gte(log => log.Timestamp, start));
        }

        if (query.EndDate.HasValue)
        {
            var end = query.EndDate.Value.Date.AddDays(1).AddTicks(-1);
            filters.Add(Builders<RequestLogDocument>.Filter.Lte(log => log.Timestamp, end));
        }

        if (!string.IsNullOrWhiteSpace(query.UserName))
        {
            filters.Add(Builders<RequestLogDocument>.Filter.Eq(log => log.Properties.ExecutedBy, query.UserName));
        }

        if (!string.IsNullOrWhiteSpace(query.Level))
        {
            filters.Add(Builders<RequestLogDocument>.Filter.Eq(log => log.Level, query.Level));
        }

        // Combine the filters using the AND operator.
        var filter = filters.Count > 0 ? Builders<RequestLogDocument>.Filter.And(filters) : Builders<RequestLogDocument>.Filter.Empty;

        // Skip and limit the logs based on the page number and page size.
        var skip = (query.PageNumber - 1) * query.PageSize;
        var limit = query.PageSize;

        // Retrieve the logs based on the filter and paging parameters.
        var logs = await _logCollection.Find(filter).Skip(skip).Limit(limit).ToListAsync();
        var totalCount = await _logCollection.CountDocumentsAsync(filter);

        // Return the logs and the total count.
        return new LogResponseDto { Logs = logs, TotalCount = totalCount };
    }

}
