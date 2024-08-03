namespace App.Domain.DTOs.ApplicationUserDtos.Response;

/// <summary>
/// Pginated response for application users
/// </summary>
public class PaginatedApplicationUserResponseDto
{
    /// <summary>
    /// Total number of users
    /// </summary>
    public int TotalUsersCount { get; set; }

    /// <summary>
    /// List of Application users
    /// </summary>
    public List<ApplicationUserResponseDto> Users { get; set; }
}
