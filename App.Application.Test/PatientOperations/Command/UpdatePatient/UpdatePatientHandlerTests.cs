using App.Application.PatientOperations.Command.UpdatePatient;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientOperations.Command.UpdatePatient;

public class UpdatePatientHandlerTests : TestHelper
{
    private readonly UpdatePatientCommand command;
    private readonly UpdatePatientHandler handler;

    public UpdatePatientHandlerTests()
    {
        command = new UpdatePatientCommand(Guid.NewGuid(),
                                           new UpdatePatientDto
                                           {
                                               Age = 18,
                                               EthnicGroup = "test",
                                               FirstName = "test",
                                               Gender = "Male",
                                               LastName = "test",
                                               Location = "test",
                                               OtherData = "test",
                                               OtherData2 = "test",
                                               OtherData3 = "test",
                                               OtherGroup = "test",
                                               YearsInSchool = 2
                                           });
        handler = new UpdatePatientHandler(patientRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdatePatient()
    {
        //Arrange
        Patient patient = new Patient("test1", "test1", "test@123", Gender.Male, "test1", "test1", 12, "test1", "test1", "test1", "test1", 2, "doctor123");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.UpdatePatient.Age, patient.Age);
        Assert.Equal(command.UpdatePatient.EthnicGroup, patient.EthnicGroup);
        Assert.Equal(command.UpdatePatient.FirstName, patient.FirstName);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingPatient_ShouldReturnFailureResult()
    {
        //Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient not found.", result.ErrorMessage);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
    }
}