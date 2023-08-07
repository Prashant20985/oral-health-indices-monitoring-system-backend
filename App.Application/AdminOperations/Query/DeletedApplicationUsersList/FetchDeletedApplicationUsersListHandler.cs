﻿using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Query.DeletedApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of deleted application users.
/// </summary>
public class FetchDeletedApplicationUsersListHandler
    : IRequestHandler<FetchDeletedApplicationUsersListQuery,
    OperationResult<PagedList<ApplicationUserDto>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchDeletedApplicationUsersListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    public FetchDeletedApplicationUsersListHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of deleted application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of deleted users.</returns>
    public async Task<OperationResult<PagedList<ApplicationUserDto>>> Handle(
        FetchDeletedApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for deleted application users
        var deletedApplicationUsersQuery = _userRepository.GetDeletedApplicationUsersQuery();

        // Apply search term filter if provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            deletedApplicationUsersQuery = deletedApplicationUsersQuery.Where(x =>
                x.UserName.Contains(request.SearchTerm) ||
                x.FirstName.Contains(request.SearchTerm));
        }

        // Create paged list of deleted application users
        var pagedDeletedApplicationUsers = await PagedList<ApplicationUserDto>
            .CreateAsync(
                source: deletedApplicationUsersQuery,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<PagedList<ApplicationUserDto>>.Success(pagedDeletedApplicationUsers);
    }
}
