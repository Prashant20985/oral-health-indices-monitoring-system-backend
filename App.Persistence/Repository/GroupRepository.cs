﻿using App.Domain.DTOs;
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
    private readonly UserContext _userContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GroupRepository class.
    /// </summary>
    /// <param name="userContext">The database context.</param>
    /// <param name="mapper">The AutoMapper instance for mapping DTOs to entities.</param>
    public GroupRepository(UserContext userContext, IMapper mapper)
    {
        _userContext = userContext;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<Group> GetGroupById(Guid groupId) => await _userContext.Groups
        .FirstOrDefaultAsync(x => x.Id == groupId);

    /// <inheritdoc />
    public async Task<Group> GetGroupByName(string groupName) => await _userContext.Groups
        .FirstOrDefaultAsync(x => x.GroupName == groupName);

    /// <inheritdoc />
    public async Task CreateGroup(Group group) =>
        await _userContext.Groups.AddAsync(group);

    /// <inheritdoc />
    public void DeleteGroup(Group group) =>
        _userContext.Groups.Remove(group);

    /// <inheritdoc />
    public async Task<StudentGroup> GetStudentGroup(string studentId, Guid groupId) => await _userContext.StudentGroups
        .FirstOrDefaultAsync(x => x.StudentId == studentId && x.GroupId == groupId);

    /// <inheritdoc />
    public async Task AddStudentToGroup(StudentGroup studentGroup) =>
        await _userContext.StudentGroups.AddAsync(studentGroup);

    /// <inheritdoc />
    public void RemoveStudentFromGroup(StudentGroup studentGroup) =>
        _userContext.StudentGroups.Remove(studentGroup);

    /// <inheritdoc />
    public async Task<List<StudentDto>> GetAllStudentsGroupedByTeacher(string teacherId) => await _userContext.Users
        .Where(x => x.StudentGroups.Any(g => g.Group.TeacherId == teacherId))
        .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
        .OrderBy(s => s.UserName)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<StudentDto>> GetAllStudentsInGroup(Guid groupId) => await _userContext.Users
        .Where(x => x.StudentGroups.Any(g => g.GroupId == groupId))
        .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
        .OrderBy(s => s.UserName)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<StudentDto>> GetAllStudentsNotInGroup(Guid groupId) => await _userContext.Users
        .Where(x => !x.StudentGroups.Any(g => g.GroupId == groupId))
        .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
        .OrderBy(s => s.UserName)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<Group>> GetAllGroupsCreatedByTeacher(string teacherId) => await _userContext.Groups
        .Where(x => x.TeacherId == teacherId)
        .ToListAsync();
}