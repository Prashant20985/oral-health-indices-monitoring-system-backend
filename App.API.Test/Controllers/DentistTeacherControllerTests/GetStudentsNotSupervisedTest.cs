﻿using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.StudentsNotSupervised;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetStudentsNotSupervisedTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetStudentsNotSupervisedTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetStudentsNotSupervised_Returns_OkResult()
    {
        //Arrange
        var teacher = new ApplicationUser("teacher@test.com", "john", "doe", "1234567890", "test");
        var student = new ApplicationUser("student@test.com", "jane", "doe", "987654321", "test123");

        var group = new Group(teacher.Id, "test");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Dentist_Teacher_Researcher"),
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Examiner"),
        }, "mock"));

        var studentsNotUnderSupervision = new List<StudentResponseDto>
        {
            new StudentResponseDto
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchStudentsNotSupervisedQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentResponseDto>>.Success(studentsNotUnderSupervision));

        _dentistTeacherController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _dentistTeacherController.GetStudentsNotSupervised();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetStudentsNotSupervised_Returns_BadRequestResult()
    {
        //Arrange
        var teacher = new ApplicationUser("teacher@test.com", "john", "doe", "1234567890", "test");
        var student = new ApplicationUser("student@test.com", "jane", "doe", "987654321", "test123");

        var group = new Group(teacher.Id, "test");

        var supervise = new Supervise(teacher.Id, student.Id);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Dentist_Teacher_Researcher"),
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Examiner"),
        }, "mock"));

        var studentsNotUnderSupervision = new List<StudentResponseDto>
        {
            new StudentResponseDto
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchStudentsNotSupervisedQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentResponseDto>>.Failure("Not Found Students not under supervision"));

        _dentistTeacherController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _dentistTeacherController.GetStudentsNotSupervised();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Not Found Students not under supervision", badRequestResult.Value);
    }
}
