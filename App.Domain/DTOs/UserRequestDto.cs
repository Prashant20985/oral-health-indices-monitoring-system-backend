namespace App.Domain.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a user request.
/// </summary>
public class UserRequestDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the user request.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the user's name associated with the request.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the title of the user request.
    /// </summary>
    public string RequestTitle { get; set; }

    /// <summary>
    /// Gets or sets the description of the user request.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the admin's comment related to the request.
    /// </summary>
    public string AdminComment { get; set; }

    /// <summary>
    /// Gets or sets the status of the user request.
    /// </summary>
    public string RequestStatus { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the request was submitted.
    /// </summary>
    public DateTime DateSubmitted { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the request was completed.
    /// </summary>
    public DateTime DateCompleted { get; set; }
}
