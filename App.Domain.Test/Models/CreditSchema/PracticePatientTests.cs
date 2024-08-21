using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticePatientTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "john.doe@example.com";
        Gender gender = Gender.Male;
        string ethnicGroup = "GroupA";
        string location = "LocationA";
        int age = 30;
        string otherGroup = "OtherGroupA";
        string otherData = "OtherDataA";
        string otherData2 = "OtherDataB";
        string otherData3 = "OtherDataC";
        int yearsInSchool = 12;

        // Act
        var practicePatient = new PracticePatient(firstName, lastName, email, gender, ethnicGroup, location, age,
                                                  otherGroup, otherData, otherData2, otherData3, yearsInSchool);

        // Assert
        Assert.NotEqual(Guid.Empty, practicePatient.Id);
        Assert.Equal(firstName, practicePatient.FirstName);
        Assert.Equal(lastName, practicePatient.LastName);
        Assert.Equal(email, practicePatient.Email);
        Assert.Equal(gender, practicePatient.Gender);
        Assert.Equal(ethnicGroup, practicePatient.EthnicGroup);
        Assert.Equal(location, practicePatient.Location);
        Assert.Equal(age, practicePatient.Age);
        Assert.Equal(otherGroup, practicePatient.OtherGroup);
        Assert.Equal(otherData, practicePatient.OtherData);
        Assert.Equal(otherData2, practicePatient.OtherData2);
        Assert.Equal(otherData3, practicePatient.OtherData3);
        Assert.Equal(yearsInSchool, practicePatient.YearsInSchool);
        Assert.Null(practicePatient.PracticePatientExaminationCard);
    }
}

