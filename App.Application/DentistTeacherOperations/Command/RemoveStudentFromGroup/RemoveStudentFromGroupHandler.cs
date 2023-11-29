using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;

/// <summary>
/// Handles the removal of a student from a group based on the provided command.
/// </summary>
internal class RemoveStudentFromGroupHandler : IRequestHandler<RemoveStudentFromGroupCommand, OperationResult<Unit>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the RemoveStudentFromGroupHandler class.
    /// </summary>
    /// <param name="groupRepository">The repository for managing group-related operations.</param>
    public RemoveStudentFromGroupHandler(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(RemoveStudentFromGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the student-group relationship.
        var studentGroup = await _groupRepository.GetStudentGroup(
            request.StudentId, request.GroupId);

        if (studentGroup is null)
            return OperationResult<Unit>.Failure("Student not in group");

        // Remove the student from the group by deleting the student-group relationship.
        _groupRepository.RemoveStudentFromGroup(studentGroup);

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
