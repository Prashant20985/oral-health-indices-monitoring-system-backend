﻿using App.Domain.DTOs.UserRequestDtos.Response;
using App.Domain.Models.Enums;
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
    /// Retrieves a list of all user requests based on request status.
    /// </summary>
    IQueryable<UserRequestResponseDto> GetAllRequestsByStatus(RequestStatus requestStatus);

    /// <summary>
    /// Retrieves a list of user requests associated with a specific user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    IQueryable<UserRequestResponseDto> GetRequestsByUserIdAndStatus(string userId, RequestStatus requestStatus);

    /// <summary>
    /// Retrieves a user request by its unique identifier.
    /// </summary>
    /// <param name="requestId">The unique identifier of the user request to retrieve.</param>
    Task<UserRequest> GetUserRequestById(Guid requestId);
}

