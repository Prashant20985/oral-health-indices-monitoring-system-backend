using App.Application.AdminOperations.Helpers;
using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;


namespace App.Application.AdminOperations.Query.ActiveApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of active application users.
/// </summary>
public class FetchActiveApplicationUsersPagedListHandler
    : IRequestHandler<FetchActiveApplicationUsersPagedListQuery,
        OperationResult<List<ApplicationUserDto>>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchActiveApplicationUsersPagedListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    public FetchActiveApplicationUsersPagedListHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of active application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of active users.</returns>
    public async Task<OperationResult<List<ApplicationUserDto>>> Handle(
        FetchActiveApplicationUsersPagedListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for active application users
        var activeApplicationUsersQuery = _userRepository.GetActiveApplicationUsersQuery();

        // Create paged list of active application users
        var filterdUsers = await QueryFilter
                .ApplyFilters(activeApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<List<ApplicationUserDto>>.Success(filterdUsers);
    }
}
