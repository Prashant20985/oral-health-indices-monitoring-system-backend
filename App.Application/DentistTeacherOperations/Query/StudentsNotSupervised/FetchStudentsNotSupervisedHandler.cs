using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotSupervised;

/// <summary>
/// Handles the fetching of students not supervised by a doctor
/// </summary>
/// <param name="superviseRepository">The repository for supervising students</param>
internal sealed class FetchStudentsNotSupervisedHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<FetchStudentsNotSupervisedQuery, OperationResult<PaginatedStudentResponseDto>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    public async Task<OperationResult<PaginatedStudentResponseDto>> Handle(FetchStudentsNotSupervisedQuery request,
        CancellationToken cancellationToken)
    {
        // Get all students not supervised by the doctor
        var query = _superviseRepository.GetAllStudentsNotUnderSupervisionByDoctorId(request.DoctorId);

        // Filter the students by name
        if (!string.IsNullOrEmpty(request.StudentName))
            query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(request.StudentName));

        // Filter the students by email
        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(x => x.Email.Contains(request.Email));

        // Get the total count of students
        var totalUsersCount = await query.CountAsync(cancellationToken);

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
