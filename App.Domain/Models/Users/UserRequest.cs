using App.Domain.Models.Enums;

namespace App.Domain.Models.Users;

/// <summary>
/// Represents a user request in the application.
/// </summary>
public class UserRequest
{
    public UserRequest(string createdBy, string requestTitle, string description)
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

    /// <summary>
    /// Updates the title and description of the user request.
    /// </summary>
    /// <param name="title">The new title for the request.</param>
    /// <param name="description">The new description for the request.</param>
    public void UpdateRequestTitleAndDescription(string title, string description)
    {
        RequestTitle = title;
        Description = description;
    }

    /// <summary>
    /// Sets the request status to "Completed" and updates related properties.
    /// </summary>
    /// <param name="adminComment">The admin comment associated with the completion.</param>
    public void SetRequestToCompleted(string adminComment)
    {
        RequestStatus = RequestStatus.Completed;
        DateCompleted = DateTime.UtcNow;
        AdminComment = adminComment;
    }

    /// <summary>
    /// Updates the request status of request to In_Progress.
    /// </summary>
    public void SetRequestToInProgress() => RequestStatus = RequestStatus.In_Progress;
    
    /// <summary>
    /// Updates the comment of the request.
    /// </summary>
    /// <param name="comment">The new comment of the request.</param>
    public void AddAdminComment(string comment) => AdminComment = comment;
}
