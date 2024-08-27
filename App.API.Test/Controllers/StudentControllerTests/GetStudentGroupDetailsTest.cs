using App.Application.Core;
using App.Application.StudentOperations.Query.StudentGroupDetails;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.StudentControllerTests;

public class GetStudentGroupDetailsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentController _studentController;

    public GetStudentGroupDetailsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentController = new TestableStudentController();
        _studentController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetStudentGroupDetails_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        var student = new ApplicationUser("student@test.com", "John", "Doe", "123456789", "test");
        var teacher = new ApplicationUser("teacher@test.com", "Jane", "Doe", "987654321", "test");

        var group = new Group(teacher.Id, "test123");
        group.Teacher = teacher;
        var studentGroup = new StudentGroup(group.Id, student.Id);
        var exam = new Exam(DateTime.Now, "test123", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group.Id);
        group.Exams.Add(exam);

        group.StudentGroups.Add(studentGroup);

        var expectedStudentGroupWithExamsListResponseDto = new StudentGroupWithExamsListResponseDto
        {
            Id = group.Id,
            GroupName = group.GroupName,
            Teacher = $"{group.Teacher.FirstName} {group.Teacher.LastName} ({group.Teacher.UserName})",
            Exams = group.Exams.Select(x => new ExamDto
            {
                DateOfExamination = x.DateOfExamination,
                ExamTitle = x.ExamTitle,
                Description = x.Description,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                DurationInterval = x.DurationInterval,
                ExamStatus = x.ExamStatus.ToString()
            }).ToList()
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentGroupDetailsWithExamsQuery>(), default))
            .ReturnsAsync(OperationResult<StudentGroupWithExamsListResponseDto>.Success(expectedStudentGroupWithExamsListResponseDto));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetStudentGroupDetails(group.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var studentGroupWithExamsListResponseDto = Assert.IsType<StudentGroupWithExamsListResponseDto>(okResult.Value);
        Assert.Equal(expectedStudentGroupWithExamsListResponseDto.Id, studentGroupWithExamsListResponseDto.Id);
        Assert.Equal(expectedStudentGroupWithExamsListResponseDto.GroupName, studentGroupWithExamsListResponseDto.GroupName);
        Assert.Equal(expectedStudentGroupWithExamsListResponseDto.Teacher, studentGroupWithExamsListResponseDto.Teacher);
        Assert.Equal(expectedStudentGroupWithExamsListResponseDto.Exams.Count, studentGroupWithExamsListResponseDto.Exams.Count);
        Assert.Equal(expectedStudentGroupWithExamsListResponseDto.Exams[0].DateOfExamination, studentGroupWithExamsListResponseDto.Exams[0].DateOfExamination);
    }

    [Fact]
    public async Task GetStudentGroupDetails_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentGroupDetailsWithExamsQuery>(), default))
            .ReturnsAsync(OperationResult<StudentGroupWithExamsListResponseDto>.Failure("No groups found for the student."));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetStudentGroupDetails(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
