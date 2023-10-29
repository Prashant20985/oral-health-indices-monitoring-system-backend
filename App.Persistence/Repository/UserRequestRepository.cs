using App.Domain.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

/// <summary>
/// Repository for managing user requests and related operations.
/// </summary>
public class UserRequestRepository : IUserRequestRepository
{
    private readonly UserContext _userContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the UserRequestRepository class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance used for mapping between entities and DTOs.</param>
    /// <param name="userContext">The database context for user-related entities.</param>
    public UserRequestRepository(IMapper mapper, UserContext userContext)
    {
        _mapper = mapper;
        _userContext = userContext;
    }

    /// <summary>
    /// Creates a new user request.
    /// </summary>
    /// <param name="userRequest">The user request to create.</param>
    public async Task CreateRequest(UserRequest userRequest) =>
        await _userContext.UserRequests.AddAsync(userRequest);

    /// <summary>
    /// Deletes a user request.
    /// </summary>
    /// <param name="userRequest">The user request to delete.</param>
    public void DeleteRequest(UserRequest userRequest) =>
        _userContext.UserRequests.Remove(userRequest);

    /// <summary>
    /// Retrieves a list of all user requests with a status of "Completed."
    /// </summary>
    public async Task<List<UserRequestDto>> GetAllCompletedRequest() => await _userContext.UserRequests
        .Where(x => x.RequestStatus.Equals(Enum.GetName(RequestStatus.Completed)))
        .ProjectTo<UserRequestDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <summary>
    /// Retrieves a list of all user requests with a status of "In Progress."
    /// </summary>
    public async Task<List<UserRequestDto>> GetAllInProgressRequest() => await _userContext.UserRequests
        .Where(x => x.RequestStatus.Equals(Enum.GetName(RequestStatus.In_Progress)))
        .ProjectTo<UserRequestDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <summary>
    /// Retrieves a list of all user requests with a status of "Submitted."
    /// </summary>
    public async Task<List<UserRequestDto>> GetAllSubmittedRequest() => await _userContext.UserRequests
        .Where(x => x.RequestStatus.Equals(Enum.GetName(RequestStatus.Submitted)))
        .ProjectTo<UserRequestDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <summary>
    /// Retrieves a list of user requests associated with a specific user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    public async Task<List<UserRequestDto>> GetRequestsByUserId(string userId) => await _userContext.UserRequests
        .Where(x => x.ApplicationUser.Id.Equals(userId))
        .ProjectTo<UserRequestDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <summary>
    /// Retrieves a user request by its unique identifier.
    /// </summary>
    /// <param name="requestId">The unique identifier of the user request to retrieve.</param>
    public async Task<UserRequestDto> GetUserRequestById(Guid requestId) => await _userContext.UserRequests
        .ProjectTo<UserRequestDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(x => x.Id.Equals(requestId));
}
