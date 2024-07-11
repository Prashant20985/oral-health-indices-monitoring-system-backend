using App.Application.Core;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentOperations.Query.SupervisingDoctors;

/// <summary>
/// Handles the Query to fetch the supervising doctors of a student.
/// </summary>
/// <param name="superviseRepository"></param>
internal sealed class FetchSupervisingDoctorsHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<FetchSupervisingDoctorsQuery, OperationResult<List<SupervisingDoctorResponseDto>>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    
    public async Task<OperationResult<List<SupervisingDoctorResponseDto>>> Handle(FetchSupervisingDoctorsQuery request, CancellationToken cancellationToken) => 
        await _superviseRepository.GetAllSupervisingDoctorsByStudentId(request.StudentId) switch
        {
            // If there are no supervising doctors, return a failure result.
            null => OperationResult<List<SupervisingDoctorResponseDto>>.Failure("No Supervising Doctors"),
            // If there are supervising doctors, return a success result with the list of supervising doctors.
            var doctors => OperationResult<List<SupervisingDoctorResponseDto>>.Success(doctors)
        };
}
