using App.Application.Core;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

/// <summary>
/// Handler for adding a practice patient examination card.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AddPracticePatientExaminationCardHandler"/> class.
/// </remarks>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class AddPracticePatientExaminationCardHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<AddPracticePatientExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to add a practice patient examination card.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(AddPracticePatientExaminationCardCommand request, CancellationToken cancellationToken)
    {
        // Check if the exam exists
        var checkExamExists = await _studentExamRepository.GetExamById(request.ExamId);
        
        if (checkExamExists is null)
            return OperationResult<Unit>.Failure("Given Exam doesn't Exists");

        // Check if the student exists
        var checkIfStudentAlreadyTookTheExam = await _studentExamRepository
            .CheckIfStudentHasAlreadyTakenTheExam(request.ExamId, request.StudentId);

        if (checkIfStudentAlreadyTookTheExam)
            return OperationResult<Unit>.Failure("Exam Already Completed");

        // Create a practice patient object
        PracticePatient practicePatient = new(
            request.CardInputModel.PatientDto.FirstName,
            request.CardInputModel.PatientDto.LastName,
            request.CardInputModel.PatientDto.Email,
            Enum.Parse<Gender>(request.CardInputModel.PatientDto.Gender),
            request.CardInputModel.PatientDto.EthnicGroup,
            request.CardInputModel.PatientDto.Location,
            request.CardInputModel.PatientDto.Age,
            request.CardInputModel.PatientDto.OtherGroup,
            request.CardInputModel.PatientDto.OtherData,
            request.CardInputModel.PatientDto.OtherData2,
            request.CardInputModel.PatientDto.OtherData3,
            request.CardInputModel.PatientDto.YearsInSchool);

        // Add practice patient to repository
        await _studentExamRepository.AddPracticePatient(practicePatient);

        // Create practice API object
        PracticeAPI practiceAPI = new(request.CardInputModel.PracticeAPI.APIResult,
            request.CardInputModel.PracticeAPI.Maxilla,
            request.CardInputModel.PracticeAPI.Mandible);

        practiceAPI.SetAssessmentModel(request.CardInputModel.PracticeAPI.AssessmentModel);

        // Add practice API bleeding to repository
        await _studentExamRepository.AddPracticeAPI(practiceAPI);

        // Create practice bleeding object
        PracticeBleeding practiceBleeding = new(request.CardInputModel.PracticeBleeding.BleedingResult,
            request.CardInputModel.PracticeBleeding.Maxilla,
            request.CardInputModel.PracticeBleeding.Mandible);

        practiceBleeding.SetAssessmentModel(request.CardInputModel.PracticeBleeding.AssessmentModel);

        // Add practice bleeding to repository
        await _studentExamRepository.AddPracticeBleeding(practiceBleeding);

        // Create practice DMFT/DMFS object
        PracticeDMFT_DMFS practiceDMFT_DMFS = new(
            request.CardInputModel.PracticeDMFT_DMFS.DMFTResult,
            request.CardInputModel.PracticeDMFT_DMFS.DMFSResult);

        practiceDMFT_DMFS.SetAssessmentModel(request.CardInputModel.PracticeDMFT_DMFS.AssessmentModel);
        practiceDMFT_DMFS.SetProstheticStatus(request.CardInputModel.PracticeDMFT_DMFS.ProstheticStatus);

        // Add practice DMFT/DMFS to repository
        await _studentExamRepository.AddPracticeDMFT_DMFS(practiceDMFT_DMFS);

        // Create practice BEWE object
        var bewe = request.CardInputModel.PracticeBewe;

        PracticeBewe practiceBewe = new(beweResult: bewe.BeweResult);

        practiceBewe.SetSectant1(bewe.Sectant1);
        practiceBewe.SetSectant2(bewe.Sectant2);
        practiceBewe.SetSectant3(bewe.Sectant3);
        practiceBewe.SetSectant4(bewe.Sectant4);
        practiceBewe.SetSectant5(bewe.Sectant5);
        practiceBewe.SetSectant6(bewe.Sectant6);

        practiceBewe.SetAssessmentModel(bewe.AssessmentModel);

        // Add practice BEWE to repository
        await _studentExamRepository.AddPracticeBewe(practiceBewe);

        // Create practice patient examination result object
        PracticePatientExaminationResult practicePatientExamiantionResult = new(
            practiceBewe.Id,
            practiceDMFT_DMFS.Id,
            practiceAPI.Id,
            practiceBleeding.Id);

        // Add practice patient examination result to repository
        await _studentExamRepository.AddPracticePatientExaminationResult(practicePatientExamiantionResult);

        // Create risk factor assessment object
        PracticeRiskFactorAssessment riskFactorAssessment = new();
        riskFactorAssessment.SetRiskFactorAssessmentModel(request.CardInputModel.RiskFactorAssessmentModel);

        // Add risk factor assessment to repository
        await _studentExamRepository.AddPracticeRiskFactorAssessment(riskFactorAssessment);

        // Create practice patient examination card object
        PracticePatientExaminationCard practicePatientExaminationCard = new(request.ExamId, request.StudentId);
        practicePatientExaminationCard.SetPatientId(practicePatient.Id);
        practicePatientExaminationCard.SetPatientExaminationResultId(practicePatientExamiantionResult.Id);
        practicePatientExaminationCard.SetRiskFactorAssessmentId(riskFactorAssessment.Id);

        practicePatientExaminationCard.SetPatientRecommendations(request.CardInputModel.Summary.PatientRecommendations);
        practicePatientExaminationCard.SetDescription(request.CardInputModel.Summary.Description);
        practicePatientExaminationCard.SetNeedForDentalInterventions(request.CardInputModel.Summary.NeedForDentalInterventions);
        practicePatientExaminationCard.SetProposedTreatment(request.CardInputModel.Summary.ProposedTreatment);

        // Add practice patient examination card to repository
        await _studentExamRepository.AddPracticePatientExaminationCard(practicePatientExaminationCard);

        // Return success with unit
        return OperationResult<Unit>.Success(Unit.Value);
    }
}

