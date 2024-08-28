using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class StudentGroupTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var studentId = "studentId";

        // Act
        var studentGroup = new StudentGroup(groupId, studentId);

        // Assert
        Assert.Equal(groupId, studentGroup.GroupId);
        Assert.Equal(studentId, studentGroup.StudentId);
        Assert.Null(studentGroup.Group);
        Assert.Null(studentGroup.Student);
    }
}