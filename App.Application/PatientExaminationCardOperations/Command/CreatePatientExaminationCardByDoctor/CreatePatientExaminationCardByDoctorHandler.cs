using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;

/// <summary>
/// Handles the creation of patient examination card by Doctor.
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
/// <param name="patientRepository">The patient repository</param>
/// <param name="userRepository">The user repository</param>
/// <param name="mapper">The mapper</param>
internal sealed class CreatePatientExaminationCardByDoctorHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IUserRepository userRepository, 
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardByDoctorCommand, OperationResult<PatientExaminationCardDto>>
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
    public async Task<OperationResult<PatientExaminationCardDto>> Handle(CreatePatientExaminationCardByDoctorCommand request,
        CancellationToken cancellationToken)
    {
        // Check if patient exists
        var checkIfPatientExists = await _patientRepository.GetPatientById(request.PatientId);

        // If patient does not exist, return failure
        if (checkIfPatientExists is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Patient not found");

        // Check if doctor exists
        var doctor = await _userRepository.GetApplicationUserWithRolesById(request.DoctorId);

        // If User doctor not exist, return failure
        if (doctor is null)
            return OperationResult<PatientExaminationCardDto>.Failure("User not found");

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.Bewe.BeweAssessmentModel);
        beweForm.CalculateBeweResult();

        // Add comment to Bewe form
        beweForm.AddDoctorComment(request.InputParams.Bewe.Comment);

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.API.APIAssessmentModel);
        apiForm.CalculateAPIResult();

        // Add comment to API form
        apiForm.AddDoctorComment(request.InputParams.API.Comment);

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.Bleeding.BleedingAssessmentModel);
        bleedingForm.CalculateBleedingResult();
        
        // Add comment to bleeding form
        bleedingForm.AddDoctorComment(request.InputParams.Bleeding.Comment);

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.DMFT_DMFS.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.SetDMFSResult(request.InputParams.DMFT_DMFS.DMFSResult);
        dmft_dmfsForm.SetDMFTResult(request.InputParams.DMFT_DMFS.DMFTResult);

        // Add comment to DMFT_DMFS form
        dmft_dmfsForm.AddDoctorComment(request.InputParams.DMFT_DMFS.Comment);

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

        // Set doctor id
        examinationCard.SetDoctorId(doctor.Id);

        // Add doctor comment
        examinationCard.AddDoctorComment(request.InputParams.PatientExaminationCardComment);

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        // Create DTO to return
        var cardToReturn = new PatientExaminationCardDto()
        {
            Id = examinationCard.Id,
            DoctorName = $"{doctor.FirstName} {doctor.LastName} ({doctor.Email})",
            StudentName = null,
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
