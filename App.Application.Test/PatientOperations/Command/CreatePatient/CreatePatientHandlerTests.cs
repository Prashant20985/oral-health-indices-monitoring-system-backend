using App.Application.PatientOperations.Command.CreatePatient;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientOperations.Command.CreatePatient;

public class CreatePatientHandlerTests : TestHelper
{
    private readonly CreatePatientCommand command;
    private readonly CreatePatientHandler handler;

    public CreatePatientHandlerTests()
    {
        command = new CreatePatientCommand(new CreatePatientDto
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@123",
            Gender = "Male",
            EthnicGroup = "test",
            Location = "test",
            Age = 12,
            OtherGroup = "test",
            OtherData = "test",
            OtherData2 = "test",
            OtherData3 = "test",
            YearsInSchool = 12
        }, "test");
        handler = new CreatePatientHandler(patientRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreatePatient()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientByEmail(It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        Assert.Equal(Unit.Value, result.ResultValue);
        patientRepositoryMock.Verify(repo => repo.GetPatientByEmail(command.CreatePatient.Email), Times.Once);
        patientRepositoryMock.Verify(repo => repo.CreatePatient(It.IsAny<Patient>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingPatient_ShouldReturnFailureResult()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientByEmail(It.IsAny<string>()))
            .ReturnsAsync(new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test", "test",
                "test", "test", 2, "doctor123"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
        Assert.Contains("Patient with this email already exists.", result.ErrorMessage);

        patientRepositoryMock.Verify(repo => repo.GetPatientByEmail(command.CreatePatient.Email), Times.Once);
        patientRepositoryMock.Verify(repo => repo.CreatePatient(It.IsAny<Patient>()), Times.Never);
    }
}