using App.Application.Core;
using App.Domain.Models.Users;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.AddStudentToGroup;

/// <summary>
/// Handles the addition of a student to a group based on the provided command.
/// </summary>
internal sealed class AddStudentToGroupHandler : IRequestHandler<AddStudentToGroupCommand, OperationResult<Unit>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the AddStudentToGroupHandler class.
    /// </summary>
    /// <param name="groupRepository">The repository for managing group-related operations.</param>
    public AddStudentToGroupHandler(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    /// <inheritdoc />
    public async Task<OperationResult<Unit>> Handle(AddStudentToGroupCommand request, CancellationToken cancellationToken)
    {
        // Check if the student is already a member of the group.
        var checkStudentAlreadyInGroup = await _groupRepository.GetStudentGroup(
            request.StudentId, request.GroupId);

        if (checkStudentAlreadyInGroup is not null)
            return OperationResult<Unit>.Failure("Student already in group");

        // Create a new student-group relationship using the provided information.
        StudentGroup studentGroup = new(request.GroupId, request.StudentId);

        // Add the newly created student-group relationship to the repository.
        await _groupRepository.AddStudentToGroup(studentGroup);

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
