using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class ResearchGroupTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var groupName = "Group A";
        var description = "Description A";
        var doctorId = "doctorId";

        // Act
        var researchGroup = new ResearchGroup(groupName, description, doctorId);

        // Assert
        Assert.Equal(groupName, researchGroup.GroupName);
        Assert.Equal(description, researchGroup.Description);
        Assert.Equal(doctorId, researchGroup.DoctorId);
        Assert.NotEqual(Guid.Empty, researchGroup.Id);
        Assert.Equal(DateTime.UtcNow.Date, researchGroup.CreatedAt.Date);
    }

    [Fact]
    public void UpdateGroup_ShouldUpdateProperties()
    {
        // Arrange
        var researchGroup = new ResearchGroup("Group A", "Description A", "doctorId");
        var newGroupName = "Group B";
        var newGroupDescription = "Description B";

        // Act
        researchGroup.UpdateGroup(newGroupName, newGroupDescription);

        // Assert
        Assert.Equal(newGroupName, researchGroup.GroupName);
        Assert.Equal(newGroupDescription, researchGroup.Description);
    }
}