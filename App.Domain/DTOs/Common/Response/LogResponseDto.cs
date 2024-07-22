using App.Domain.Models.Logs;

namespace App.Domain.DTOs.Common.Response;

public class LogResponseDto
{
    public List<RequestLogDocument> Logs { get; set; }
    public long TotalCount { get; set; }
}
