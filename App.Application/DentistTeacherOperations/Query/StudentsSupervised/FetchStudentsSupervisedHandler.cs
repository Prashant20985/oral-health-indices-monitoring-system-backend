using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentsSupervised;

/// <summary>
/// Handles the fetching of all students supervised by a doctor
/// </summary>
/// <param name="superviseRepository">The repository for supervising students</param>
internal sealed class FetchStudentsSupervisedHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<FetchStudentsSupervisedQuery, OperationResult<List<StudentResponseDto>>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    public async Task<OperationResult<List<StudentResponseDto>>> Handle(FetchStudentsSupervisedQuery request,
        CancellationToken cancellationToken) =>
        OperationResult<List<StudentResponseDto>>.Success(
            await _superviseRepository.GetAllStudentsUnderSupervisionByDoctorId(request.DoctorId));
}

