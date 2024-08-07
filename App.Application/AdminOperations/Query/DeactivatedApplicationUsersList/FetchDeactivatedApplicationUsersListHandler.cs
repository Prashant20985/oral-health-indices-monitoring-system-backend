﻿using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;


namespace App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of deactivated application users.
/// </summary>
internal sealed class FetchDeactivatedApplicationUsersListHandler
    : IRequestHandler<FetchDeactivatedApplicationUsersListQuery,
    OperationResult<PaginatedApplicationUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationUsersListQuesyFilter _queryFilter;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchDeactivatedApplicationUsersListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="queryFilter">The query filter instance.</param>
    public FetchDeactivatedApplicationUsersListHandler(IUserRepository userRepository,
        IApplicationUsersListQuesyFilter queryFilter)
    {
        _userRepository = userRepository;
        _queryFilter = queryFilter;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of deactivated application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of deactivated users.</returns>
    public async Task<OperationResult<PaginatedApplicationUserResponseDto>> Handle(
        FetchDeactivatedApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for deactivated application users
        var deactivatedApplicationUsersQuery = _userRepository.GetDeactivatedApplicationUsersQuery();

        // Create paged list of deactivated application users
        var filteredUsers = await _queryFilter
                .ApplyFilters(deactivatedApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<PaginatedApplicationUserResponseDto>.Success(filteredUsers);
    }
}
