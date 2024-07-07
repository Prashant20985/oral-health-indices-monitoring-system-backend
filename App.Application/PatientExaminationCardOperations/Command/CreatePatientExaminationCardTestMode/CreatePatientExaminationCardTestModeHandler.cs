using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

/// <summary>
/// Handler to create a patient examination card in test mode
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
/// <param name="patientRepository">The patient repository</param>
/// <param name="userRepository">The user repository</param>
/// <param name="mapper">The mapper</param>
internal sealed class CreatePatientExaminationCardTestModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IUserRepository userRepository,
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardTestModeCommand, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    private readonly IPatientRepository _patientRepository = patientRepository;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Handles the creation of a patient examination card in test mode
    /// </summary>
    /// <param name="request">The request to create a patient examination card in test mode</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<PatientExaminationCardDto>> Handle(CreatePatientExaminationCardTestModeCommand request, CancellationToken cancellationToken)
    {
        // Check if the patient exists
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // If the patient does not exist, return an error
        if (patient is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Patient not found");

        // Check if the student exists
        var student = await _userRepository.GetApplicationUserWithRolesById(request.StudentId);

        if (student is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Student not found");

        // Check if the student is a student
        if (!student.ApplicationUserRoles.Any(x => x.ApplicationRole.Name.Equals("Student")))
            return OperationResult<PatientExaminationCardDto>.Failure("User is not a student");

        // Check if the doctor exists
        var doctor = await _userRepository.GetApplicationUserWithRolesById(request.InputParams.DoctorId);

        if (doctor is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Doctor not found");

        // Check if the doctor is a doctor
        if (!doctor.ApplicationUserRoles.Any(x => x.ApplicationRole.Name.Equals("Dentist_Teacher_Examiner")
            || x.ApplicationRole.Name.Equals("Dentist_Teacher_Researcher")))
            return OperationResult<PatientExaminationCardDto>.Failure("User is not a doctor");

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.CreateBeweRequest.BeweAssessmentModel);
        beweForm.SetBeweResult(request.InputParams.CreateBeweRequest.BeweResult);
        beweForm.AddStudentComment(request.InputParams.CreateBeweRequest.StudentComment);

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.CreateAPIRequest.APIAssessmentModel);
        apiForm.SetAPIResult(request.InputParams.CreateAPIRequest.APIResult);
        apiForm.SetMaxilla(request.InputParams.CreateAPIRequest.Maxilla);
        apiForm.SetMandible(request.InputParams.CreateAPIRequest.Mandible);
        apiForm.AddStudentComment(request.InputParams.CreateAPIRequest.StudentComment);

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.CreateBleedingRequest.BleedingAssessmentModel);
        bleedingForm.SetBleedingResult(request.InputParams.CreateBleedingRequest.BleedingResult);
        bleedingForm.SetMaxilla(request.InputParams.CreateBleedingRequest.Maxilla);
        bleedingForm.SetMandible(request.InputParams.CreateBleedingRequest.Mandible);
        bleedingForm.AddStudentComment(request.InputParams.CreateBleedingRequest.StudentComment);

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.CreateDMFT_DMFSRequest.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.SetDMFTResult(request.InputParams.CreateDMFT_DMFSRequest.DMFTResult);
        dmft_dmfsForm.SetDMFSResult(request.InputParams.CreateDMFT_DMFSRequest.DMFSResult);
        dmft_dmfsForm.AddStudentComment(request.InputParams.CreateDMFT_DMFSRequest.StudentComment);

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
        examinationCard.SetStudentId(request.StudentId);
        examinationCard.SetDoctorId(request.InputParams.DoctorId);
        examinationCard.SetPatientExaminationResultId(patientExaminationResult.Id);
        examinationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);
        examinationCard.AddStudentComment(request.InputParams.PatientExaminationCardCommentByStudent);

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        // Create the DTO to return
        var cardToReturn = new PatientExaminationCardDto()
        {
            Id = examinationCard.Id,
            DoctorName = $"{doctor.FirstName} {doctor.LastName} ({doctor.Email})",
            PatientName = $"{patient.FirstName} {patient.LastName} ({patient.Email})",
            StudentName = $"{student.FirstName} {student.LastName} ({student.Email})",
            StudentComment = examinationCard.StudentComment,
            DateOfExamination = examinationCard.DateOfExamination,
            DoctorComment = examinationCard.DoctorComment,
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
