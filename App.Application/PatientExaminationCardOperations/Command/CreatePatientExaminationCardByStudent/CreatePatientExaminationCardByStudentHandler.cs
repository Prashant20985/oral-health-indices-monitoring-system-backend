using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByStudent;

/// <summary>
/// Handler for creating patient examination card by student
/// </summary>
/// <param name="mapper">The mapper</param>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
/// <param name="patientRepository">The patient repository</param>
/// <param name="userRepository">The user repository</param>
internal sealed class CreatePatientExaminationCardByStudentHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IUserRepository userRepository,
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardByStudentCommand, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    private readonly IPatientRepository _patientRepository = patientRepository;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<OperationResult<PatientExaminationCardDto>> Handle(CreatePatientExaminationCardByStudentCommand request, CancellationToken cancellationToken)
    {
        // Check if patient exists
        var checkIfPatientExists = await _patientRepository.GetPatientById(request.PatientId);

        // If patient does not exist, return failure
        if (checkIfPatientExists is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Patient not found");

        // Check if student exists
        var student = await _userRepository.GetApplicationUserWithRolesById(request.StudentId);

        // If User student not exist, return failure
        if (student is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Student not found");

        var doctor = await _userRepository.GetApplicationUserWithRolesById(request.InputParams.AssignedDoctorId);

        // If User doctor not exist, return failure
        if (doctor is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Doctor not found");

        // check if doctor is doctor
        if (!doctor.ApplicationUserRoles.Any(x => x.ApplicationRole.Name.Equals("Dentist_Teacher_Examiner")
                || x.ApplicationRole.Name.Equals("Dentist_Teacher_Researcher")))
            return OperationResult<PatientExaminationCardDto>.Failure("Invalid User");

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.Bewe.BeweAssessmentModel);
        beweForm.CalculateBeweResult();

        // Add comment to Bewe form
        beweForm.AddStudentComment(request.InputParams.Bewe.Comment);

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.API.APIAssessmentModel);
        apiForm.CalculateAPIResult();

        // Add comment to API form
        apiForm.AddStudentComment(request.InputParams.API.Comment);

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.Bleeding.BleedingAssessmentModel);
        bleedingForm.CalculateBleedingResult();

        // Add comment to bleeding form
        bleedingForm.AddStudentComment(request.InputParams.Bleeding.Comment);

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.DMFT_DMFS.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.SetDMFTResult(request.InputParams.DMFT_DMFS.DMFTResult);
        dmft_dmfsForm.SetDMFSResult(request.InputParams.DMFT_DMFS.DMFSResult);

        // Add comment to DMFT_DMFS form
        dmft_dmfsForm.AddStudentComment(request.InputParams.DMFT_DMFS.Comment);

        // Add DMFT_DMFS form to repository
        await _patientExaminationCardRepository.AddDMFT_DMFS(dmft_dmfsForm);

        // Create patient examination result
        var patientExaminationResult = new PatientExaminationResult(
            beweId: beweForm.Id,
            dMFT_DMFSId: dmft_dmfsForm.Id,
            aPIId: apiForm.Id,
            bleedingId: bleedingForm.Id);

        // Add patient examination result to repository
        await _patientExaminationCardRepository.AddPatientExaminationResult(patientExaminationResult);

        // Create risk factor assessment
        var riskFactorAssessment = new RiskFactorAssessment();
        riskFactorAssessment.SetRiskFactorAssessmentModel(request.InputParams.RiskFactorAssessmentModel);

        // Add risk factor assessment to repository
        await _patientExaminationCardRepository.AddRiskFactorAssessment(riskFactorAssessment);

        // Create patient examination card
        var examinationCard = new PatientExaminationCard(request.PatientId);
        examinationCard.SetTestMode();
        examinationCard.SetPatientExaminationResultId(patientExaminationResult.Id);
        examinationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        // Set doctor id
        examinationCard.SetDoctorId(doctor.Id);

        // Set Student id
        examinationCard.SetStudentId(student.Id);

        // Add doctor comment
        examinationCard.AddStudentComment(request.InputParams.PatientExaminationCardComment);

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        var cardToReturn = new PatientExaminationCardDto()
        {
            Id = examinationCard.Id,
            DoctorName = $"{doctor.FirstName} {doctor.LastName} ({doctor.Email})",
            StudentName = $"{student.FirstName} {student.LastName} ({student.Email})",
            PatientName = $"{checkIfPatientExists.FirstName} {checkIfPatientExists.LastName} ({checkIfPatientExists.Email})",
            DateOfExamination = examinationCard.DateOfExamination,
            DoctorComment = examinationCard.DoctorComment,
            TotalScore = examinationCard.TotalScore,
            StudentComment = examinationCard.StudentComment,
            IsRegularMode = examinationCard.IsRegularMode,
            RiskFactorAssessment = _mapper.Map<RiskFactorAssessmentDto>(riskFactorAssessment),
            PatientExaminationResult = new PatientExaminationResultDto()
            {
                Bewe = _mapper.Map<BeweResponseDto>(beweForm),
                API = _mapper.Map<APIResponseDto>(apiForm),
                Bleeding = _mapper.Map<BleedingResponseDto>(bleedingForm),
                DMFT_DMFS = _mapper.Map<DMFT_DMFSResponseDto>(dmft_dmfsForm)
            }
        };

        return OperationResult<PatientExaminationCardDto>.Success(cardToReturn);
    }
}
