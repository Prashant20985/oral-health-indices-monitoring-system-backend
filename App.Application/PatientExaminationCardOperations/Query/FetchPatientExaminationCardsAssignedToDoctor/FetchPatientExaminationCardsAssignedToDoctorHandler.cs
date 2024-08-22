using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;

/// <summary>
/// Handler for fetching patient examination cards assigned to a doctor.
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository.</param>
/// <param name="mapper">The mapper.</param>
internal sealed class FetchPatientExaminationCardsAssignedToDoctorHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository
    , IMapper mapper)
    : IRequestHandler<FetchPatientExaminationCardsAssignedToDoctorQuery, OperationResult<List<PatientDetailsWithExaminationCards>>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<OperationResult<List<PatientDetailsWithExaminationCards>>> Handle(FetchPatientExaminationCardsAssignedToDoctorQuery request, CancellationToken cancellationToken)
    {
        // Get all patient examination cards assigned to the doctor.
        var query = _patientExaminationCardRepository.GetPatientExaminationCardAssignedToDoctor(request.DoctorId);

        // Filter the cards by student ID, year, and month.
        if (request.StudentId is not null)
            query = query.Where(card => card.StudentId == request.StudentId);

        // If the year and month are not specified, filter the cards by the current year and month.
        // Otherwise, filter the cards by the specified year and month.
        if (request.Year is not 0)
            query = query.Where(card => card.DateOfExamination.Year == request.Year);
        else
            query = query.Where(card => card.DateOfExamination.Year == DateTime.Now.Year);

        // If the month is not specified, filter the cards by the current month.
        // Otherwise, filter the cards by the specified month.
        if (request.Month is not 0)
            query = query.Where(card => card.DateOfExamination.Month == request.Month);
        else
            query = query.Where(card => card.DateOfExamination.Month == DateTime.Now.Month);

        // Project the cards to the response DTO.
        var cards = await query
            .Include(x => x.Doctor)
            .Include(x => x.Student)
            .ProjectTo<PatientDetailsWithExaminationCards>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Return the cards list.
        return OperationResult<List<PatientDetailsWithExaminationCards>>.Success(cards);
    }
}