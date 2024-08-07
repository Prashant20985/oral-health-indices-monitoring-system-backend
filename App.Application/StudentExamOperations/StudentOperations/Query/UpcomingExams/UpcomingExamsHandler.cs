using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;

internal sealed class UpcomingExamsHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<UpcominExamsQuery, OperationResult<List<ExamDto>>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    public async Task<OperationResult<List<ExamDto>>> Handle(UpcominExamsQuery request,
        CancellationToken cancellationToken) => OperationResult<List<ExamDto>>.Success(
                await _studentExamRepository.UpcomingExams(request.StudentId));
}
