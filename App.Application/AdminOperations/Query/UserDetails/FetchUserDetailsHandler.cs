using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.AdminOperations.Query.UserDetails;

/// <summary>
/// Handler for fetching details about a specific user.
/// </summary>
internal sealed class FetchUserDetailsHandler
    : IRequestHandler<FetchUserDetailsQuery,
        OperationResult<ApplicationUserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchUserDetailsHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="mapper">The Automapper instance</param>
    public FetchUserDetailsHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request by retrieving details about a specific user.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the details of the user.</returns>
    public async Task<OperationResult<ApplicationUserDto>> Handle(
        FetchUserDetailsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the user by username or email
        var user = await _userRepository.GetUserByUserNameOrEmail(request.UserName, cancellationToken);

        // If user is not found, return failure result
        if (user is null)
            return OperationResult<ApplicationUserDto>.Failure("User not found");

        // Map the user to ApplicationUserDto
        var mappedUser = _mapper.Map<ApplicationUserDto>(user);

        // Return the mapped user details as a successful operation result
        return OperationResult<ApplicationUserDto>.Success(mappedUser);
    }
}
