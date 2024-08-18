using App.Application.Core;
using App.Application.PatientOperations.Query.PatientDetails;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class FetchPatientDetailsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public FetchPatientDetailsTest()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task FetchPatientDetails_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var expectedPatient = new PatientResponseDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example",
            Gender = Gender.Male.ToString(),
            EthnicGroup = "Caucasian",
            OtherGroup = "Other",
            YearsInSchool = 1,
            OtherData = "Other data",
            OtherData2 = "Other data 2",
            OtherData3 = "Other data 3",
            Location = "Location",
            Age = 20
        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchPatientDetailsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<PatientResponseDto>.Success(expectedPatient));

        // Act
        var result = await _patientcontroller.FetchPatientDetails(expectedPatient.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualPatient = Assert.IsType<PatientResponseDto>(okResult.Value);
        Assert.Equal(expectedPatient.Id, actualPatient.Id);
        Assert.Equal(expectedPatient.FirstName, actualPatient.FirstName);
        Assert.Equal(expectedPatient.LastName, actualPatient.LastName);
        Assert.Equal(expectedPatient.Email, actualPatient.Email);
    }

    [Fact]
    public async Task FetchPatientDetails_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var expectedPatient = new PatientResponseDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example",
            Gender = Gender.Male.ToString(),
            EthnicGroup = "Caucasian",
            OtherGroup = "Other",
            YearsInSchool = 1,
            OtherData = "Other data",
            OtherData2 = "Other data 2",
            OtherData3 = "Other data 3",
            Location = "Location",
            Age = 20
        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchPatientDetailsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<PatientResponseDto>.Failure("Patient not found"));


        // Act
        var result = await _patientcontroller.FetchPatientDetails(Guid.NewGuid());

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Patient not found", badRequest.Value);
    }
}
