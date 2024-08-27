using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;

public class ExaminationCardsAssignedToDoctorHandlerTests : TestHelper
{
    private readonly FetchPatientExaminationCardsAssignedToDoctorHandler handler;

    public ExaminationCardsAssignedToDoctorHandlerTests()
    {
        handler = new FetchPatientExaminationCardsAssignedToDoctorHandler(patientExaminationCardRepositoryMock.Object, mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsPatientExaminationCardsAssignedToDoctor()
    {
        // Arrange
        var doctorId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;

        var card = new PatientExaminationCard(Guid.NewGuid());
        card.SetDoctorId(doctorId);
        card.SetStudentId(studentId);
        card.SetTestMode();

        var queryableExaminationCards = new List<PatientExaminationCard>
            {
                card
            }.AsQueryable().BuildMockDbSet();

        patientExaminationCardRepositoryMock
            .Setup(repo => repo.GetPatientExaminationCardAssignedToDoctor(It.IsAny<string>()))
            .Returns(queryableExaminationCards.Object);

        mapperMock
            .Setup(mapper => mapper.ConfigurationProvider)
            .Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientExaminationCard, PatientDetailsWithExaminationCards>();
                cfg.CreateMap<Patient, PatientResponseDto>();
                cfg.CreateMap<RiskFactorAssessment, RiskFactorAssessmentDto>();
                cfg.CreateMap<PatientExaminationResult, PatientExaminationResultDto>();
                cfg.CreateMap<Bewe, BeweResponseDto>();
                cfg.CreateMap<DMFT_DMFS, DMFT_DMFSResponseDto>();
                cfg.CreateMap<API, APIResponseDto>();
                cfg.CreateMap<Bleeding, BleedingResponseDto>();
            }));

        var request = new FetchPatientExaminationCardsAssignedToDoctorQuery
        (
            doctorId,
            studentId,
            year,
            month
        );

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Single(result.ResultValue);
        var card1 = result.ResultValue.First();
    }

    [Fact]
    public async Task Handle_ReturnsEmptyListWhenNoMatchingCards()
    {
        // Arrange
        var doctorId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;

        var queryableExaminationCards = new List<PatientExaminationCard>().AsQueryable().BuildMockDbSet();

        patientExaminationCardRepositoryMock
            .Setup(repo => repo.GetPatientExaminationCardAssignedToDoctor(It.IsAny<string>()))
            .Returns(queryableExaminationCards.Object);

        mapperMock
            .Setup(mapper => mapper.ConfigurationProvider)
            .Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientExaminationCard, PatientDetailsWithExaminationCards>();
                cfg.CreateMap<Patient, PatientResponseDto>();
                cfg.CreateMap<RiskFactorAssessment, RiskFactorAssessmentDto>();
                cfg.CreateMap<PatientExaminationResult, PatientExaminationResultDto>();
                cfg.CreateMap<Bewe, BeweResponseDto>();
                cfg.CreateMap<DMFT_DMFS, DMFT_DMFSResponseDto>();
                cfg.CreateMap<API, APIResponseDto>();
                cfg.CreateMap<Bleeding, BleedingResponseDto>();
            }));

        var request = new FetchPatientExaminationCardsAssignedToDoctorQuery
        (
            doctorId,
            studentId,
            year,
            month
        );

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Empty(result.ResultValue);
    }
}
