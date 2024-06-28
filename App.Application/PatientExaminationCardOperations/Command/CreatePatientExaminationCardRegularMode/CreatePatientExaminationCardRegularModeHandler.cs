using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using AutoMapper;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Handles the creation of patient examination card in regular mode
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
/// <param name="patientRepository">The patient repository</param>
/// <param name="mapper">The mapper</param>
internal sealed class CreatePatientExaminationCardRegularModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository,
    IMapper mapper)
    : IRequestHandler<CreatePatientExaminationCardRegularModeCommand, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    private readonly IPatientRepository _patientRepository = patientRepository;

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

        // Create Bewe form
        var beweForm = new Bewe();
        beweForm.SetAssessmentModel(request.InputParams.BeweAssessmentModel);
        beweForm.CalculateBeweResult();

        // Add Bewe form to repository
        await _patientExaminationCardRepository.AddBewe(beweForm);

        // Create API form
        var apiForm = new API();
        apiForm.SetAssessmentModel(request.InputParams.APIAssessmentModel);
        apiForm.CalculateAPIResult();

        // Add API form to repository
        await _patientExaminationCardRepository.AddAPI(apiForm);

        // Create Bleeding form
        var bleedingForm = new Bleeding();
        bleedingForm.SetAssessmentModel(request.InputParams.BleedingAssessmentModel);
        bleedingForm.CalculateBleedingResult();

        // Add Bleeding form to repository
        await _patientExaminationCardRepository.AddBleeding(bleedingForm);

        // Create DMFT_DMFS form
        var dmft_dmfsForm = new DMFT_DMFS();
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.InputParams.DMFT_DMFSAssessmentModel);
        dmft_dmfsForm.CalculateDMFSResult();
        dmft_dmfsForm.CalculateDMFTResult();

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
        examinationCard.SetDcotorId(request.InputParams.DoctorId);
        examinationCard.SetPatientExaminationResultId(patientExaminationResult.Id);
        examinationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        // Add patient examination card to repository
        await _patientExaminationCardRepository.AddPatientExaminationCard(examinationCard);

        // Create DTO to return
        PatientExaminationCardDto cardToReturn = new()
        {
            Id = examinationCard.Id,
            DoctorComment = examinationCard.DoctorComment,
            IsRegularMode = examinationCard.IsRegularMode,
            TotalScore = examinationCard.TotalScore,
            DateOfExamination = examinationCard.DateOfExamination,
            RiskFactorAssessment = _mapper.Map<RiskFactorAssessmentDto>(riskFactorAssessment),
            PatientExaminationResult = new PatientExaminationResultDto
            {
                DMFT_DMFS = _mapper.Map<DMFT_DMFSDto>(dmft_dmfsForm),
                API = _mapper.Map<APIDto>(apiForm),
                Bleeding = _mapper.Map<BleedingDto>(bleedingForm),
                Bewe = _mapper.Map<BeweDto>(beweForm)
            }
        };

        return OperationResult<PatientExaminationCardDto>.Success(cardToReturn);
    }
}
