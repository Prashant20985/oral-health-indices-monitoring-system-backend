﻿using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
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
    public async Task DeleteGroup(Guid groupId)
    {
        var groupToDelete = await _oralEhrContext.Groups
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatient)
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticeRiskFactorAssessment)
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.Bleeding)
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.Bewe)
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.API)
            .Include(x => x.Exams)
            .ThenInclude(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.DMFT_DMFS)
            .FirstOrDefaultAsync(x => x.Id == groupId);

        if (groupToDelete is not null)
        {
            _oralEhrContext.Groups.Remove(groupToDelete);

            if (groupToDelete.Exams.Count != 0)
            {
                _oralEhrContext.Exams.RemoveRange(groupToDelete.Exams);

                foreach (var exam in groupToDelete.Exams)
                {
                    if (exam.PracticePatientExaminationCards.Count != 0)
                    {
                        _oralEhrContext.PracticePatientExaminationCards.RemoveRange(exam.PracticePatientExaminationCards);

                        foreach (var card in exam.PracticePatientExaminationCards)
                        {
                            _oralEhrContext.PracticeRiskFactorAssessments.Remove(card.PracticeRiskFactorAssessment);
                            _oralEhrContext.PracticePatients.Remove(card.PracticePatient);
                            _oralEhrContext.PracticeAPIs.Remove(card.PracticePatientExaminationResult.API);
                            _oralEhrContext.PracticeBleedings.Remove(card.PracticePatientExaminationResult.Bleeding);
                            _oralEhrContext.PracticeBewes.Remove(card.PracticePatientExaminationResult.Bewe);
                            _oralEhrContext.PracticeDMFT_DMFSs.Remove(card.PracticePatientExaminationResult.DMFT_DMFS);
                        }
                    }
                }
            }
        }
    }

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
    public IQueryable<StudentResponseDto> GetAllStudentsNotInGroup(Guid groupId) => _oralEhrContext.Users
        .Where(x => !x.StudentGroups.Any(g => g.GroupId == groupId)
            && x.ApplicationUserRoles.Any(r => r.ApplicationRole.Name.Equals("Student")))
        .ProjectTo<StudentResponseDto>(_mapper.ConfigurationProvider)
        .OrderBy(s => s.UserName)
        .AsQueryable();

    /// <inheritdoc />
    public async Task<List<Group>> GetAllGroupsCreatedByTeacher(string teacherId) => await _oralEhrContext.Groups
        .Where(x => x.TeacherId == teacherId)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<List<StudentGroupResponseDto>> GetAllGroupsWithStudentsList(string teacherId)
    {
        return await _oralEhrContext.Groups
            .Where(x => x.TeacherId.Equals(teacherId))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new StudentGroupResponseDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                Students = x.StudentGroups.Select(x => _mapper.Map<StudentResponseDto>(x.Student)).ToList()
            })
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<StudentGroupResponseDto> GetGroupDetailsWithStudentList(Guid groupId)
    {
        return await _oralEhrContext.Groups
            .Where(x => x.Id == groupId)
            .Select(x => new StudentGroupResponseDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                Students = x.StudentGroups.Select(x => _mapper.Map<StudentResponseDto>(x.Student)).ToList()
            }).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<List<StudentGroupWithExamsListResponseDto>> GetAllGroupsByStudentIdWithExamsList(string studentId)
    {
        return await _oralEhrContext.Groups
            .Where(x => x.StudentGroups.Any(s => s.StudentId == studentId))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new StudentGroupWithExamsListResponseDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                Teacher = $"{x.Teacher.FirstName} {x.Teacher.LastName} ({x.Teacher.UserName})",
                Exams = x.Exams.Select(x => _mapper.Map<ExamDto>(x)).ToList()
            })
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<StudentGroupWithExamsListResponseDto> GetGroupDetailsWithExamsListByGroupIdAndStudentId(Guid groupId, string studentId)
    {
        return await _oralEhrContext.Groups
            .Where(x => x.Id == groupId && x.StudentGroups.Any(s => s.StudentId == studentId))
            .Select(x => new StudentGroupWithExamsListResponseDto
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedAt = x.CreatedAt,
                Teacher = $"{x.Teacher.FirstName} {x.Teacher.LastName} ({x.Teacher.UserName})",
                Exams = x.Exams.Select(x => _mapper.Map<ExamDto>(x)).ToList()
            }).FirstOrDefaultAsync();
    }
}
