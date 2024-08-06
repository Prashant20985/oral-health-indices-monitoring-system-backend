using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.StudentsSupervised;

/// <summary>
/// Handles the fetching of all students supervised by a doctor
/// </summary>
/// <param name="superviseRepository">The repository for supervising students</param>
internal sealed class FetchStudentsSupervisedHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<FetchStudentsSupervisedQuery, OperationResult<PaginatedStudentResponseDto>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    public async Task<OperationResult<PaginatedStudentResponseDto>> Handle(FetchStudentsSupervisedQuery request,
        CancellationToken cancellationToken)
    {
        // Get all students supervised by the doctor
        var query = _superviseRepository.GetAllStudentsUnderSupervisionByDoctorId(request.DoctorId);

        // Filter the students by name
        if (!string.IsNullOrEmpty(request.StudentName))
            query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(request.StudentName));

        // Filter the students by email
        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(x => x.Email.Contains(request.Email));

        // Get the total count of students
        var totalUsersCount = await query.CountAsync(cancellationToken);

        // Get the students for the current page
        var students = await query
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Create the paginated response
        var paginatedResponse = new PaginatedStudentResponseDto
        {
            TotalStudents = totalUsersCount,
            Students = students
        };

        // Return the paginated response
        return OperationResult<PaginatedStudentResponseDto>.Success(paginatedResponse);
    }
}

