using App.Domain.DTOs;
using App.Domain.Models.Users;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

/// <summary>
/// Repository for managing groups and related operations in the database.
/// </summary>
public class GroupRepository : IGroupRepository
{
    private readonly OralEhrContext _oralEhrContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GroupRepository class.
    /// </summary>
    /// <param name="oralEhrContext">The database context.</param>
    /// <param name="mapper">The AutoMapper instance for mapping DTOs to entities.</param>
    public GroupRepository(OralEhrContext oralEhrContext, IMapper mapper)
    {
        _oralEhrContext = oralEhrContext;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<Group> GetGroupById(Guid groupId) => await _oralEhrContext.Groups
        .FirstOrDefaultAsync(x => x.Id == groupId);

    /// <inheritdoc />
    public async Task<Group> GetGroupByName(string groupName) => await _oralEhrContext.Groups
        .FirstOrDefaultAsync(x => x.GroupName == groupName);

    /// <inheritdoc />
    public async Task CreateGroup(Group group) =>
        await _oralEhrContext.Groups.AddAsync(group);

    /// <inheritdoc />
    public void DeleteGroup(Group group) =>
        _oralEhrContext.Groups.Remove(group);

    /// <inheritdoc />
    public async Task<StudentGroup> GetStudentGroup(string studentId, Guid groupId) => await _oralEhrContext.StudentGroups
        .FirstOrDefaultAsync(x => x.StudentId == studentId && x.GroupId == groupId);

    /// <inheritdoc />
    public async Task AddStudentToGroup(StudentGroup studentGroup) =>
        await _oralEhrContext.StudentGroups.AddAsync(studentGroup);

    /// <inheritdoc />
    public void RemoveStudentFromGroup(StudentGroup studentGroup) =>
        _oralEhrContext.StudentGroups.Remove(studentGroup);

    /// <inheritdoc />
    public async Task<List<StudentDto>> GetAllStudentsNotInGroup(Guid groupId) => await _oralEhrContext.Users
        .Where(x => !x.StudentGroups.Any(g => g.GroupId == groupId)
            && x.ApplicationUserRoles.Any(r => r.ApplicationRole.Name.Equals("Student")))
        .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
        .OrderBy(s => s.UserName)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<Group>> GetAllGroupsCreatedByTeacher(string teacherId) => await _oralEhrContext.Groups
        .Where(x => x.TeacherId == teacherId)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<GroupDto>> GetAllGroupsWithStudentsList(string teacherId)
    {
        return await _oralEhrContext.Groups
            .Where(x => x.TeacherId.Equals(teacherId))
            .Select(x => new GroupDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                Students = x.StudentGroups.Select(x => _mapper.Map<StudentDto>(x.Student)).ToList()
            }).ToListAsync();
    }
}
