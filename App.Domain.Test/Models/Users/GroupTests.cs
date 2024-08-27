using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class GroupTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var teacherId = "teacherId";
        var groupName = "Group A";

        // Act
        var group = new Group(teacherId, groupName);

        // Assert
        Assert.Equal(teacherId, group.TeacherId);
        Assert.Equal(groupName, group.GroupName);
        Assert.NotEqual(Guid.Empty, group.Id);
        Assert.Equal(DateTime.UtcNow.Date, group.CreatedAt.Date);
        Assert.NotNull(group.StudentGroups);
        Assert.Empty(group.StudentGroups);
        Assert.NotNull(group.Exams);
        Assert.Empty(group.Exams);
    }

    [Fact]
    public void UpdateGroupName_ShouldUpdateGroupName()
    {
        // Arrange
        var group = new Group("teacherId", "Group A");
        var newGroupName = "Group B";

        // Act
        group.UpdateGroupName(newGroupName);

        // Assert
        Assert.Equal(newGroupName, group.GroupName);
    }
}
