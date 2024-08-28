using System.Security.Claims;
using App.Application.Core;
using App.Application.PatientOperations.Command.CreatePatient;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class CreatePatientTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TestablePatientController _patientController;

    public CreatePatientTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _patientController = new TestablePatientController();
        _patientController.ExposeSetMediator(_mediatorMock.Object);
    }

    public TestablePatientController Get_patientController()
    {
        return _patientController;
    }

    [Fact]
    public async Task CreatePatient_Returns_OkResult()
    {
        //Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var createPatientDto = new CreatePatientDto
        {
            FirstName = "TestPatient",
            LastName = "TestSurname",
            Email = "Test@test.com",
            Gender = Gender.Male.ToString(),
            EthnicGroup = "TestEthnicGroup",
            OtherGroup = "TestOtherGroup",
            YearsInSchool = 1,
            OtherData = "TestOtherData",
            OtherData2 = "TestOtherData2",
            OtherData3 = "TestOtherData3",
            Location = "TestLocation",
            Age = 20
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePatientCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _patientController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _patientController.CreatePatient(createPatientDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}