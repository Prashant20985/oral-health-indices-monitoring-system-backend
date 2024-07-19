using App.API.Controllers;
using App.Application.Core;
using App.Application.StudentOperations.Query.StudentGroupsList;
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

public class GetStudentGroupsWithExamsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentController _studentController;

    public GetStudentGroupsWithExamsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentController = new TestableStudentController();
        _studentController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetStudentGroupsWithExams_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));


        var student = new ApplicationUser("student@test.com", "John", "Doe", "123456789", "test");
        var teacher = new ApplicationUser("teacher@test.com","Jane","Doe","987654321","test");

        var group = new Group(teacher.Id,"test123");
        group.Teacher = teacher;
        var studentGroup = new StudentGroup(group.Id, student.Id);
        var exam = new Exam(DateTime.Now, "test123", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group.Id);
        group.Exams.Add(exam);
        
        group.StudentGroups.Add(studentGroup);

        var group2 = new Group(teacher.Id, "test234");
        group2.Teacher = teacher;
        var studentGroup2 = new StudentGroup(group2.Id, student.Id);
        var exam2 = new Exam(DateTime.Now.AddDays(1), "test234", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group2.Id);
        group2.StudentGroups.Add(studentGroup2);
        group2.Exams.Add(exam2);

        var studentGroupWithExamsDtos = new List<StudentGroupWithExamsListResponseDto>
        {
            new StudentGroupWithExamsListResponseDto {
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
                    PublishDate = x.PublishDate,
                    DurationInterval = x.DurationInterval,
                    MaxMark = x.MaxMark,
                    ExamStatus = x.ExamStatus.ToString()
                }).ToList()
            },
            new StudentGroupWithExamsListResponseDto {
                Id = group2.Id,
                GroupName = group2.GroupName,
                Teacher = $"{group2.Teacher.FirstName} {group2.Teacher.LastName} ({group2.Teacher.UserName})",
                Exams = group2.Exams.Select(x => new ExamDto
                {
                    DateOfExamination = x.DateOfExamination,
                    ExamTitle = x.ExamTitle,
                    Description = x.Description,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    PublishDate = x.PublishDate,
                    DurationInterval = x.DurationInterval,
                    MaxMark = x.MaxMark,
                    ExamStatus = x.ExamStatus.ToString()
                }).ToList()
            }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentGroupsWithExamsListQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentGroupWithExamsListResponseDto>>.Success(studentGroupWithExamsDtos));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetStudentGroupsWithExams();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<StudentGroupWithExamsListResponseDto>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetStudentGroupsWithExams_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentGroupsWithExamsListQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentGroupWithExamsListResponseDto>>.Failure("No groups found for the student."));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.GetStudentGroupsWithExams();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
