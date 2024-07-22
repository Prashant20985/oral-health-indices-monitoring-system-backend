using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.Models.Users;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace App.Persistence.Repository;

/// <summary>
/// Represents a repository for supervise-related operations.
/// </summary>
/// <param name="mapper">The mapper.</param>
/// <param name="oralEhrContext">The ORAL EHR context.</param>
public class SuperviseRepository(OralEhrContext oralEhrContext, IMapper mapper) : ISuperviseRepository
{
    private readonly OralEhrContext _oralEhrContext = oralEhrContext;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc />
    public async Task AddSupervise(Supervise supervise) =>
        await _oralEhrContext.Supervises.AddAsync(supervise);

    /// <inheritdoc />
    public async Task<bool> CheckStudentAlreadyUnderDoctorSupervison(string studentId, string doctorId) =>
        await _oralEhrContext.Supervises.AnyAsync(s => s.StudentId == studentId && s.DoctorId == doctorId);

    /// <inheritdoc />
    public async Task<List<StudentResponseDto>> GetAllStudentsUnderSupervisionByDoctorId(string doctorId) =>
        await _oralEhrContext.Supervises
            .Where(s => s.DoctorId == doctorId)
            .Select(s => s.Student)
            .Where(s => s.IsAccountActive && s.DeletedAt == null)
            .ProjectTo<StudentResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<List<SupervisingDoctorResponseDto>> GetAllSupervisingDoctorsByStudentId(string studentId) =>
        await _oralEhrContext.Supervises
            .Where(s => s.StudentId == studentId)
            .Select(s => s.Doctor)
            .Where(s => s.IsAccountActive && s.DeletedAt == null)
            .ProjectTo<SupervisingDoctorResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    /// <inheritdoc />
    public void RemoveSupervise(Supervise supervise) =>
        _oralEhrContext.Supervises.Remove(supervise);

    /// <inheritdoc />
    public async Task<Supervise> GetSuperviseByDoctorIdAndStudentId(string doctorId, string studentId) =>
        await _oralEhrContext.Supervises
            .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.StudentId == studentId);

    /// <inheritdoc />
    public async Task<List<StudentResponseDto>> GetAllStudentsNotUnderSupervisionByDoctorId(string doctorId) =>
        await _oralEhrContext.Users
            .Where(x => x.IsAccountActive && x.DeletedAt == null
                && x.ApplicationUserRoles.Any(r => r.ApplicationRole.Name.Equals("Student"))
                && x.SuperviseStudentNavigation.All(s => s.DoctorId != doctorId))
            .ProjectTo<StudentResponseDto>(_mapper.ConfigurationProvider)
            .OrderBy(s => s.UserName)
        .ToListAsync();
}
