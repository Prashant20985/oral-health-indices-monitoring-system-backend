using App.Domain.DTOs;
using App.Domain.Models.Users;

namespace App.Domain.Repository;

/// <summary>
/// Repository interface for managing user requests and related operations.
/// </summary>
public interface IUserRequestRepository
{
    /// <summary>
    /// Creates a new user request.
    /// </summary>
    /// <param name="userRequest">The user request to create.</param>
    Task CreateRequest(UserRequest userRequest);

    /// <summary>
    /// Deletes a user request.
    /// </summary>
    /// <param name="userRequest">The user request to delete.</param>
    void DeleteRequest(UserRequest userRequest);

    /// <summary>
    /// Retrieves a list of all user requests that have been submitted.
    /// </summary>
    Task<List<UserRequestDto>> GetAllSubmittedRequest();

    /// <summary>
    /// Retrieves a list of all completed user requests.
    /// </summary>
    Task<List<UserRequestDto>> GetAllCompletedRequest();

    /// <summary>
    /// Retrieves a list of all user requests that are in progress.
    /// </summary>
    Task<List<UserRequestDto>> GetAllInProgressRequest();

    /// <summary>
    /// Retrieves a list of user requests associated with a specific user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    Task<List<UserRequestDto>> GetRequestsByUserId(string userId);

    /// <summary>
    /// Retrieves a user request by its unique identifier.
    /// </summary>
    /// <param name="requestId">The unique identifier of the user request to retrieve.</param>
    Task<UserRequestDto> GetUserRequestById(Guid requestId);
}

