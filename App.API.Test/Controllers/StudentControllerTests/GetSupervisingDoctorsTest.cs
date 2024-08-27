using App.Application.Core;
using App.Application.StudentOperations.Query.SupervisingDoctors;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.StudentControllerTests;

public class GetSupervisingDoctorsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentController _studentController;

    public GetSupervisingDoctorsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentController = new TestableStudentController();
        _studentController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetSupervisingDoctors_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        var student = new ApplicationUser("student@test.com", "John", "Doe", "123456789", "test");
        var teacher = new ApplicationUser("teacher@test.com", "Jane", "Doe", "987654321", "test");

        var supervisor1 = new ApplicationUser("supervisor1@test.com", "Supervisor1", "Doe", "123456789", "test");
        var supervisor2 = new ApplicationUser("supervisor2@test.com", "Supervisor2", "Doe", "987654321", "test");

        var studentSupervisor1 = new Supervise(student.Id, supervisor1.Id);
        var studentSupervisor2 = new Supervise(student.Id, supervisor2.Id);

        var expectedSupervisingDoctors = new List<SupervisingDoctorResponseDto>
        {
            new SupervisingDoctorResponseDto
            {
                Id = supervisor1.Id,
                DoctorName = $"{supervisor1.FirstName} {supervisor1.LastName}"
            },
            new SupervisingDoctorResponseDto
            {
                Id = supervisor2.Id,
                DoctorName = $"{supervisor2.FirstName} {supervisor2.LastName}"
            }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchSupervisingDoctorsQuery>(), default))
            .ReturnsAsync(OperationResult<List<SupervisingDoctorResponseDto>>.Success(expectedSupervisingDoctors));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetSupervisingDoctors();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<SupervisingDoctorResponseDto>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetSupervisingDoctors_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        _mediator.Setup(x => x.Send(It.IsAny<FetchSupervisingDoctorsQuery>(), default))
            .ReturnsAsync(OperationResult<List<SupervisingDoctorResponseDto>>.Failure("No Supervising doctors found"));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetSupervisingDoctors();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
