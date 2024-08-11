using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.Users;

namespace App.Domain.Repository;

/// <summary>
/// Repository interface for managing groups and related operations.
/// </summary>
public interface IGroupRepository
{
    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="group">The group to create.</param>
    Task CreateGroup(Group group);

    /// <summary>
    /// Adds a student to a group.
    /// </summary>
    /// <param name="studentGroup">The relationship between a student and a group to add.</param>
    Task AddStudentToGroup(StudentGroup studentGroup);

    /// <summary>
    /// Removes a student from a group.
    /// </summary>
    /// <param name="studentGroup">The relationship between a student and a group to remove.</param>
    void RemoveStudentFromGroup(StudentGroup studentGroup);

    /// <summary>
    /// Gets the relationship between a student and a group by student and group identifiers.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <param name="groupId">The identifier of the group.</param>
    Task<StudentGroup> GetStudentGroup(string studentId, Guid groupId);

    /// <summary>
    /// Deletes a group.
    /// </summary>
    /// <param name="group">The group to delete.</param>
    Task DeleteGroup(Guid groupId);

    /// <summary>
    /// Gets a group by its name.
    /// </summary>
    /// <param name="groupName">The name of the group to retrieve.</param>
    Task<Group> GetGroupByName(string groupName);

    /// <summary>
    /// Gets a group by its identifier.
    /// </summary>
    /// <param name="groupId">The identifier of the group to retrieve.</param>
    Task<Group> GetGroupById(Guid groupId);

    /// <summary>
    /// Gets all groups created by a teacher.
    /// </summary>
    /// <param name="teacherId">The identifier of the teacher.</param>
    Task<List<Group>> GetAllGroupsCreatedByTeacher(string teacherId);

    /// <summary>
    /// Gets a list of students not in a specific group.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    IQueryable<StudentResponseDto> GetAllStudentsNotInGroup(Guid groupId);

    /// <summary>
    /// Gets a list of groups with associated students, grouped by a teacher.
    /// </summary>
    /// <param name="teacherId">The identifier of the teacher.</param>
    /// <returns>A list of GroupDto objects representing groups and their associated students.</returns>
    Task<List<StudentGroupResponseDto>> GetAllGroupsWithStudentsList(string teacherId);

    /// <summary>
    /// Gets the details of a group with a list of students.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    /// <returns>A GroupDto object representing the group and its associated students.</returns>
    Task<StudentGroupResponseDto> GetGroupDetailsWithStudentList(Guid groupId);

    /// <summary>
    /// Gets a list of groups with associated exams, by student identifier.
    /// </summary>
    /// <param name="studentId">The identifier of the student.</param>
    /// <returns>A list of GroupWithExamsListDto objects representing groups and their associated exams.</returns>
    Task<List<StudentGroupWithExamsListResponseDto>> GetAllGroupsByStudentIdWithExamsList(string studentId);

    /// <summary>
    /// Gets the details of a group with a list of exams, by group and student identifier.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    /// <param name="studentId">The identifier of the student.</param>
    /// <returns>A GroupWithExamsListDto object representing the group and its associated exams.</returns>
    Task<StudentGroupWithExamsListResponseDto> GetGroupDetailsWithExamsListByGroupIdAndStudentId(Guid groupId, string studentId);
}
