using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Handles the creation of patient examination card in regular mode by a student or a doctor user.
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
/// <param name="patientRepository">The patient repository</param>
/// <param name="userRepository">The user repository</param>
/// <param name="mapper">The mapper</param>
internal sealed class CreatePatientExaminationCardRegularModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IUserRepository userRepository, 
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardRegularModeCommand, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    private readonly IPatientRepository _patientRepository = patientRepository;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Handles the creation of patient examination card in regular mode
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The operation result</returns>
    public async Task<OperationResult<PatientExaminationCardDto>> Handle(CreatePatientExaminationCardRegularModeCommand request, CancellationToken cancellationToken)
    {
        // Check if patient exists
        var checkIfPatientExists = await _patientRepository.GetPatientById(request.PatientId);

        // If patient does not exist, return failure
        if (checkIfPatientExists is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Patient not found");

        // Check if User exists
        var user = await _userRepository.GetApplicationUserWithRolesById(request.UserId);

        // If User does not exist, return failure
        if (user is null)
            return OperationResult<PatientExaminationCardDto>.Failure("User not found");

        // Verify Assigned Doctor
        if (!string.IsNullOrEmpty(request.InputParams.AssignedDoctorId))
        {
            var doctor = await _userRepository.GetApplicationUserWithRolesById(request.InputParams.AssignedDoctorId);

            // If doctor does not exist, return failure
            if (doctor is null)
                return OperationResult<PatientExaminationCardDto>.Failure("Doctor not found");

            // If doctor is not a dentist teacher examiner or researcher, return failure
            if (!doctor.ApplicationUserRoles.Any(x => x.ApplicationRole.Name.Equals("Dentist_Teacher_Examiner") 
                    || x.ApplicationRole.Name.Equals("Dentist_Teacher_Researcher")))
                return OperationResult<PatientExaminationCardDto>.Failure("Assigned user is not a doctor");
        }

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.CreateBeweRequest.BeweAssessmentModel);
        beweForm.CalculateBeweResult();

        // Add comment to Bewe form
        if (request.IsStudent) 
            beweForm.AddStudentComment(request.InputParams.CreateBeweRequest.Comment);
        else
            beweForm.AddDoctorComment(request.InputParams.CreateBeweRequest.Comment);

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.CreateAPIRequest.APIAssessmentModel);
        apiForm.CalculateAPIResult();

        // Add comment to API form
        if(request.IsStudent)
            apiForm.AddStudentComment(request.InputParams.CreateAPIRequest.Comment);
        else
            apiForm.AddDoctorComment(request.InputParams.CreateAPIRequest.Comment);

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.CreateBleedingRequest.BleedingAssessmentModel);
        bleedingForm.CalculateBleedingResult();
        
        // Add comment to bleeding form
        if(request.IsStudent)
            bleedingForm.AddStudentComment(request.InputParams.CreateBleedingRequest.Comment);
        else
            bleedingForm.AddDoctorComment(request.InputParams.CreateBleedingRequest.Comment);

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.CreateDMFT_DMFSRequest.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.CalculateDMFSResult();
        dmft_dmfsForm.CalculateDMFTResult();

        // Add comment to DMFT_DMFS form
        if(request.IsStudent)
            dmft_dmfsForm.AddStudentComment(request.InputParams.CreateDMFT_DMFSRequest.Comment);
        else
            dmft_dmfsForm.AddDoctorComment(request.InputParams.CreateDMFT_DMFSRequest.Comment);

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
        examinationCard.SetRegularMode();
        examinationCard.SetPatientExaminationResultId(patientExaminationResult.Id);
        examinationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        if (request.IsStudent)
        {
            // Set student id and doctor id
            examinationCard.SetStudentId(user.Id);
            examinationCard.SetDoctorId(request.InputParams.AssignedDoctorId);

            // Add student comment
            examinationCard.AddStudentComment(request.InputParams.PatientExaminationCardComment);
        }
        else
        {
            // Set doctor id
            examinationCard.SetDoctorId(user.Id);

            // Add doctor comment
            examinationCard.AddDoctorComment(request.InputParams.PatientExaminationCardComment);
        }

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        // Create DTO to return
        var cardToReturn = new PatientExaminationCardDto()
        {
            Id = examinationCard.Id,
            DoctorName = $"{user.FirstName} {user.LastName} ({user.Email})",
            StudentName = request.IsStudent ? $"{user.FirstName} {user.LastName} ({user.Email})" : null,
            PatientName = $"{checkIfPatientExists.FirstName} {checkIfPatientExists.LastName} ({checkIfPatientExists.Email})",
            DateOfExamination = examinationCard.DateOfExamination,
            DoctorComment = examinationCard.DoctorComment,
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
