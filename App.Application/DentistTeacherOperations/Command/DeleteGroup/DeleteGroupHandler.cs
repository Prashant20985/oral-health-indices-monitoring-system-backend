using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.DeleteGroup;

/// <summary>
/// Handles the removal of a group based on the provided command.
/// </summary>
internal sealed class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, OperationResult<Unit>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteGroupHandler class
    /// </summary>
    /// <param name="groupRepository">The repository for managing group-related operations.</param>
    public DeleteGroupHandler(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrive the group.
        var group = await _groupRepository.GetGroupById(request.GroupId);

        if (group == null)
            return OperationResult<Unit>.Failure("Group Id not found");

        // Remove the group.
        _groupRepository.DeleteGroup(group);

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
