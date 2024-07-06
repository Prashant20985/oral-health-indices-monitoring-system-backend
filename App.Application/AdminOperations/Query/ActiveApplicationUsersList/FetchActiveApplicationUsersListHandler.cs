using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;


namespace App.Application.AdminOperations.Query.ActiveApplicationUsersList;

/// <summary>
/// Handler for fetching a paged list of active application users.
/// </summary>
internal sealed class FetchActiveApplicationUsersListHandler
    : IRequestHandler<FetchActiveApplicationUsersListQuery,
        OperationResult<List<ApplicationUserResponseDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IQueryFilter _queryFilter;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchActiveApplicationUsersListHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="queryFilter">The query filter instance.</param>
    public FetchActiveApplicationUsersListHandler(IUserRepository userRepository,
        IQueryFilter queryFilter)
    {
        _queryFilter = queryFilter;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the request by retrieving a paged list of active application users.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the paged list of active users.</returns>
    public async Task<OperationResult<List<ApplicationUserResponseDto>>> Handle(
        FetchActiveApplicationUsersListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the query for active application users
        var activeApplicationUsersQuery = _userRepository.GetActiveApplicationUsersQuery();

        // Create paged list of active application users
        var filterdUsers = await _queryFilter
                .ApplyFilters(activeApplicationUsersQuery, request.Params, cancellationToken);

        // Return the paged list as a successful operation result
        return OperationResult<List<ApplicationUserResponseDto>>.Success(filterdUsers);
    }
}
