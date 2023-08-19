using App.Application.AdminOperations.Helpers;
using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Query.DeletedApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of deleted application users.
/// </summary>
public class FetchDeletedApplicationUsersListHandler
    : IRequestHandler<FetchDeletedApplicationUsersListQuery,
    OperationResult<List<ApplicationUserDto>>>
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
    public async Task<OperationResult<List<ApplicationUserDto>>> Handle(
        FetchDeletedApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for deleted application users
        var deletedApplicationUsersQuery = _userRepository.GetDeletedApplicationUsersQuery();

        // Create paged list of deleted application users and apply filters from search params 
        var filteredUsers = await QueryFilter
                .ApplyFilters(deletedApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<List<ApplicationUserDto>>.Success(filteredUsers);
    }
}
