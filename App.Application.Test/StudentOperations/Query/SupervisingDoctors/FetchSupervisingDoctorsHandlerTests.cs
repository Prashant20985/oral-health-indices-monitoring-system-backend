using App.Application.StudentOperations.Query.SupervisingDoctors;
using App.Domain.DTOs.SuperviseDtos.Response;
using Moq;

namespace App.Application.Test.StudentOperations.Query.SupervisingDoctors;

public class FetchSupervisingDoctorsHandlerTests : TestHelper
{
    private readonly FetchSupervisingDoctorsHandler handler;
    private readonly FetchSupervisingDoctorsQuery query;

    public FetchSupervisingDoctorsHandlerTests()
    {
        handler = new FetchSupervisingDoctorsHandler(superviseRepositoryMock.Object);
        query = new FetchSupervisingDoctorsQuery("studentId");
    }

    [Fact]
    public async Task Handle_WhenSupervisingDoctorsExist_ShouldReturnListOfSupervisingDoctors()
    {
        // Arrange
        var supervisingDoctors = new List<SupervisingDoctorResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorName = "doctor"
            }
        };

        superviseRepositoryMock.Setup(x => x.GetAllSupervisingDoctorsByStudentId(It.IsAny<string>()))
            .ReturnsAsync(supervisingDoctors);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(supervisingDoctors.Count, result.ResultValue.Count);
        Assert.Equal(supervisingDoctors[0].Id, result.ResultValue[0].Id);
        Assert.Equal(supervisingDoctors[0].DoctorName, result.ResultValue[0].DoctorName);
    }

    [Fact]
    public async Task Handle_WhenSupervisingDoctorsDoNotExist_ShouldReturnFailure()
    {
        // Arrange
        superviseRepositoryMock.Setup(x => x.GetAllSupervisingDoctorsByStudentId(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("No Supervising Doctors", result.ErrorMessage);
    }
}