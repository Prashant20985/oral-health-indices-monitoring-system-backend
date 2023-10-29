using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UpdateGroupName;

/// <summary>
/// Handles the update of group name based on the provided command.
/// </summary>
internal sealed class UpdateGroupNameHandler : IRequestHandler<UpdateGroupNameCommand, OperationResult<Unit>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Intializes a new instance of the UpdateGroupNameHandler class
    /// </summary>
    /// <param name="groupRepository">The repository for managing group-related operations.</param>
    public UpdateGroupNameHandler(IGroupRepository groupRepository) => _groupRepository = groupRepository;


    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(UpdateGroupNameCommand request, CancellationToken cancellationToken)
    {
        // Retrive the group.
        var group = await _groupRepository.GetGroupById(request.GroupId);

        if (group is null)
            return OperationResult<Unit>.Failure("Group not found");

        var checkGroupName = await _groupRepository.GetGroupByName(request.GroupName);

        if (checkGroupName is not null)
            return OperationResult<Unit>.Failure("Group name already taken");

        // Update the name of a group.
        group.UpdateGroupName(request.GroupName);

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}

