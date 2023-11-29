using App.Application.Core;
using App.Domain.Models.Users;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.CreateGroup;

/// <summary>
/// Handles the creation of a new group based on the provided command.
/// </summary>
internal sealed class CreateGroupHandler : IRequestHandler<CreateGroupCommand, OperationResult<Unit>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the CreateGroupHandler class.
    /// </summary>
    /// <param name="groupRepository">The repository for managing group-related operations.</param>
    public CreateGroupHandler(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        // Check if a group with the same name already exists.
        var checkGroupAlreadyExists = await _groupRepository.GetGroupByName(request.GroupName);

        if (checkGroupAlreadyExists is not null)
            return OperationResult<Unit>.Failure("Group already exists");

        // Create a new group using the provided information.
        Group group = new(request.TeacherId, request.GroupName);

        // Add the newly created group to the repository.
        await _groupRepository.CreateGroup(group);

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
