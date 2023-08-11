using App.Application.AdminOperations.Helpers;
using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;


namespace App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of deactivated application users.
/// </summary>
public class FetchDeactivatedApplicationUsersListHandler
    : IRequestHandler<FetchDeactivatedApplicationUsersListQuery,
    OperationResult<PagedList<ApplicationUserDto>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchDeactivatedApplicationUsersListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    public FetchDeactivatedApplicationUsersListHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of deactivated application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of deactivated users.</returns>
    public async Task<OperationResult<PagedList<ApplicationUserDto>>> Handle(
        FetchDeactivatedApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for deactivated application users
        var deactivatedApplicationUsersQuery = _userRepository.GetDeactivatedApplicationUsersQuery();

        // Create paged list of deactivated application users
        var pagedDeactivatedApplicationUsers = await QueryFilter
                .ApplyFilters(deactivatedApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<PagedList<ApplicationUserDto>>.Success(pagedDeactivatedApplicationUsers);
    }
}
