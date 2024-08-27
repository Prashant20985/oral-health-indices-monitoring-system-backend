using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class PatientTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var gender = Gender.Male;
        var ethnicGroup = "GroupA";
        var location = "LocationA";
        var age = 30;
        var otherGroup = "OtherGroupA";
        var otherData = "OtherDataA";
        var otherData2 = "OtherDataB";
        var otherData3 = "OtherDataC";
        var yearsInSchool = 10;
        var doctorId = "doctorId";

        // Act
        var patient = new Patient(firstName, lastName, email, gender, ethnicGroup, location, age, otherGroup, otherData,
            otherData2, otherData3, yearsInSchool, doctorId);

        // Assert
        Assert.Equal(firstName, patient.FirstName);
        Assert.Equal(lastName, patient.LastName);
        Assert.Equal(email, patient.Email);
        Assert.Equal(gender, patient.Gender);
        Assert.Equal(ethnicGroup, patient.EthnicGroup);
        Assert.Equal(location, patient.Location);
        Assert.Equal(age, patient.Age);
        Assert.Equal(otherGroup, patient.OtherGroup);
        Assert.Equal(otherData, patient.OtherData);
        Assert.Equal(otherData2, patient.OtherData2);
        Assert.Equal(otherData3, patient.OtherData3);
        Assert.Equal(yearsInSchool, patient.YearsInSchool);
        Assert.Equal(doctorId, patient.DoctorId);
    }

    [Fact]
    public void ArchivePatient_ShouldSetIsArchivedAndArchiveComment()
    {
        // Arrange
        var patient = new Patient("John", "Doe", "john.doe@example.com", Gender.Male, "GroupA", "LocationA", 30,
            "OtherGroupA", "OtherDataA", "OtherDataB", "OtherDataC", 10, "doctorId");

        var comment = "Archived for testing";

        // Act
        patient.ArchivePatient(comment);

        // Assert
        Assert.True(patient.IsArchived);
        Assert.Equal(comment, patient.ArchiveComment);
    }

    [Fact]
    public void UnarchivePatient_ShouldUnsetIsArchivedAndArchiveComment()
    {
        // Arrange
        var patient = new Patient("John", "Doe", "john.doe@example.com", Gender.Male, "GroupA", "LocationA", 30,
            "OtherGroupA", "OtherDataA", "OtherDataB", "OtherDataC", 10, "doctorId");

        patient.ArchivePatient("Archived for testing");

        // Act
        patient.UnarchivePatient();

        // Assert
        Assert.False(patient.IsArchived);
        Assert.Null(patient.ArchiveComment);
    }

    [Fact]
    public void UpdatePatient_ShouldUpdateProperties()
    {
        // Arrange
        var patient = new Patient("John", "Doe", "john.doe@example.com", Gender.Male, "GroupA", "LocationA", 30,
            "OtherGroupA", "OtherDataA", "OtherDataB", "OtherDataC", 10, "doctorId");

        var newFirstName = "Jane";
        var newLastName = "Smith";
        var newGender = "Female";
        var newEthnicGroup = "GroupB";
        var newLocation = "LocationB";
        var newAge = 25;
        var newOtherGroup = "OtherGroupB";
        var newOtherData = "OtherDataD";
        var newOtherData2 = "OtherDataE";
        var newOtherData3 = "OtherDataF";
        var newYearsInSchool = 12;

        // Act
        patient.UpdatePatient(
            newFirstName,
            newLastName,
            newGender,
            newEthnicGroup,
            newLocation,
            newAge,
            newOtherGroup,
            newOtherData,
            newOtherData2,
            newOtherData3,
            newYearsInSchool);

        // Assert
        Assert.Equal(newFirstName, patient.FirstName);
        Assert.Equal(newLastName, patient.LastName);
        Assert.Equal(Gender.Female, patient.Gender);
        Assert.Equal(newEthnicGroup, patient.EthnicGroup);
        Assert.Equal(newLocation, patient.Location);
        Assert.Equal(newAge, patient.Age);
        Assert.Equal(newOtherGroup, patient.OtherGroup);
        Assert.Equal(newOtherData, patient.OtherData);
        Assert.Equal(newOtherData2, patient.OtherData2);
        Assert.Equal(newOtherData3, patient.OtherData3);
        Assert.Equal(newYearsInSchool, patient.YearsInSchool);
    }

    [Fact]
    public void AddResearchGroup_ShouldSetResearchGroupId()
    {
        // Arrange
        var patient = new Patient("John", "Doe", "john.doe@example.com", Gender.Male, "GroupA", "LocationA", 30,
            "OtherGroupA", "OtherDataA", "OtherDataB", "OtherDataC", 10, "doctorId");

        var researchGroupId = Guid.NewGuid();

        // Act
        patient.AddResearchGroup(researchGroupId);

        // Assert
        Assert.Equal(researchGroupId, patient.ResearchGroupId);
    }

    [Fact]
    public void RemoveResearchGroup_ShouldUnsetResearchGroupId()
    {
        // Arrange
        var patient = new Patient("John", "Doe", "john.doe@example.com", Gender.Male, "GroupA", "LocationA", 30,
            "OtherGroupA", "OtherDataA", "OtherDataB", "OtherDataC", 10, "doctorId");

        var researchGroupId = Guid.NewGuid();
        patient.AddResearchGroup(researchGroupId);

        // Act
        patient.RemoveResearchGroup();

        // Assert
        Assert.Null(patient.ResearchGroupId);
    }
}