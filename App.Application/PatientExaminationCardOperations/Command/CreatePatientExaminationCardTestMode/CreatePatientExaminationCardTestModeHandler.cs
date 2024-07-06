using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

internal sealed class CreatePatientExaminationCardTestModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardTestModeCommand, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    private readonly IPatientRepository _patientRepository = patientRepository;

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

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.BeweAssessmentModel);
        beweForm.SetBeweResult(request.InputParams.BeweResult);

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.APIAssessmentModel);
        apiForm.SetAPIResult(request.InputParams.APIResult);

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.BleedingAssessmentModel);
        bleedingForm.SetBleedingResult(request.InputParams.BleedingResult);

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.SetDMFTResult(request.InputParams.DMFT_Result);
        dmft_dmfsForm.SetDMFSResult(request.InputParams.DMFS_Result);

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
        examinationCard.SetStudentId(request.InputParams.StudentId);
        examinationCard.SetDcotorId(request.InputParams.DoctorId);
        examinationCard.SetPatientExaminationResultId(patientExaminationResult.Id);
        examinationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        // Create the DTO to return
        PatientExaminationCardDto cardToReturn = new()
        {
            Id = examinationCard.Id,
            DoctorComment = examinationCard.DoctorComment,
            IsRegularMode = examinationCard.IsRegularMode,
            TotalScore = examinationCard.TotalScore,
            DateOfExamination = examinationCard.DateOfExamination,
            RiskFactorAssessment = _mapper.Map<RiskFactorAssessmentDto>(riskFactorAssessment),
            PatientExaminationResult = new PatientExaminationResultDto()
            {
                DMFT_DMFS = _mapper.Map<DMFT_DMFSResponseDto>(dmft_dmfsForm),
                API = _mapper.Map<APIResponseDto>(apiForm),
                Bleeding = _mapper.Map<BleedingResponseDto>(bleedingForm),
                Bewe = _mapper.Map<BeweResponseDto>(beweForm)
            }
        };

        return OperationResult<PatientExaminationCardDto>.Success(cardToReturn);
    }
}
