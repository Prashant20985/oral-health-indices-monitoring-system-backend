﻿using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Query.DeletedApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of deleted application users.
/// </summary>
internal sealed class FetchDeletedApplicationUsersListHandler
    : IRequestHandler<FetchDeletedApplicationUsersListQuery,
    OperationResult<PaginatedApplicationUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationUsersListQuesyFilter _queryFilter;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchDeletedApplicationUsersListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="queryFilter">The query filter instance.</param>
    public FetchDeletedApplicationUsersListHandler(IUserRepository userRepository,
        IApplicationUsersListQuesyFilter queryFilter)
    {
        _userRepository = userRepository;
        _queryFilter = queryFilter;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of deleted application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of deleted users.</returns>
    public async Task<OperationResult<PaginatedApplicationUserResponseDto>> Handle(
        FetchDeletedApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for deleted application users
        var deletedApplicationUsersQuery = _userRepository.GetDeletedApplicationUsersQuery();

        // Create paged list of deleted application users and apply filters from search params 
        var filteredUsers = await _queryFilter
                .ApplyFilters(deletedApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<PaginatedApplicationUserResponseDto>.Success(filteredUsers);
    }
}
