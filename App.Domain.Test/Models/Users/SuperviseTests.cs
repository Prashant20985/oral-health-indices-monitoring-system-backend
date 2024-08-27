using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class SuperviseTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var doctorId = "doctor123";
        var studentId = "student123";

        // Act
        var supervise = new Supervise(doctorId, studentId);

        // Assert
        Assert.NotEqual(Guid.Empty, supervise.Id);
        Assert.Equal(doctorId, supervise.DoctorId);
        Assert.Equal(studentId, supervise.StudentId);
        Assert.Null(supervise.Doctor);
        Assert.Null(supervise.Student);
    }
}