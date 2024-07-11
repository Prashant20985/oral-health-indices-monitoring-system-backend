using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotSupervised;

/// <summary>
/// Handles the fetching of students not supervised by a doctor
/// </summary>
/// <param name="superviseRepository">The repository for supervising students</param>
internal sealed class FetchStudentsNotSupervisedHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<FetchStudentsNotSupervisedQuery, OperationResult<List<StudentResponseDto>>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    public async Task<OperationResult<List<StudentResponseDto>>> Handle(FetchStudentsNotSupervisedQuery request,
        CancellationToken cancellationToken) => 
        OperationResult<List<StudentResponseDto>>.Success(
            await _superviseRepository.GetAllStudentsNotUnderSupervisionByDoctorId(request.DoctorId));
}
