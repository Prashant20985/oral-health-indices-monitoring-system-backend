using App.Domain.Models.Enums;

namespace App.Domain.Models.Users;

/// <summary>
/// Represents a user request in the application.
/// </summary>
public class UserRequest
{
    public UserRequest(string createdBy, string requestTitle,string description)
    {
        Id = Guid.NewGuid();
        CreatedBy = createdBy;
        RequestTitle = requestTitle;
        Description = description;
    }
    
    /// <summary>
    /// Gets or sets the unique identifier for the user request.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the username or identifier of the user who created the request.
    /// </summary>
    public string CreatedBy { get; private set; }

    /// <summary>
    /// Gets or sets an instance of the ApplicationUser class associated with this request.
    /// </summary>
    public ApplicationUser ApplicationUser { get; private set; }

    /// <summary>
    /// Gets or sets the title of the user request.
    /// </summary>
    public string RequestTitle { get; private set; }

    /// <summary>
    /// Gets or sets the description or details of the user request.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets or sets comments provided by an administrator regarding the request.
    /// </summary>
    public string AdminComment { get; private set; } = null;

    /// <summary>
    /// Gets or sets the status of the user request, using the RequestStatus enumeration.
    /// </summary>
    public RequestStatus RequestStatus { get; private set; } = RequestStatus.Submitted;

    /// <summary>
    /// Gets or sets the date and time when the request was submitted.
    /// </summary>
    public DateTime DateSubmitted { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the request was completed.
    /// </summary>
    public DateTime DateCompleted { get; private set; }
}
