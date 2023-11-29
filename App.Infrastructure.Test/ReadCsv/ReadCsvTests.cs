using CsvHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace App.Infrastructure.Test.ReadCsv;

public class ReadCsvTests
{
    [Fact]
    public void ReadApplicationUsersFromCsv_ValidCsv_ReturnsListOfUsers()
    {
        // Arrange
        var csvData = "FirstName,LastName,Email,PhoneNumber,GuestUserComment\n" +
                      "John,Doe,john@example.com,1234567890,\n" +
                      "Alice,Smith,alice@example.com,9876543210,Some comment";

        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(csvData);
        writer.Flush();
        stream.Position = 0;

        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
        fileMock.Setup(f => f.FileName).Returns("users.csv");
        fileMock.Setup(f => f.Length).Returns(stream.Length);

        var readCsv = new Infrastructure.ReadCsv.ReadCsv();

        // Act
        var users = readCsv.ReadApplicationUsersFromCsv(fileMock.Object);

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count);

        // Check user data
        Assert.Equal("John", users[0].FirstName);
        Assert.Equal("Doe", users[0].LastName);
        Assert.Equal("john@example.com", users[0].Email);
        Assert.Equal("1234567890", users[0].PhoneNumber);
        Assert.Equal("", users[0].GuestUserComment);

        Assert.Equal("Alice", users[1].FirstName);
        Assert.Equal("Smith", users[1].LastName);
        Assert.Equal("alice@example.com", users[1].Email);
        Assert.Equal("9876543210", users[1].PhoneNumber);
        Assert.Equal("Some comment", users[1].GuestUserComment);
    }

    [Fact]
    public void ReadApplicationUsersFromCsv_InvalidCsv_ThrowsHeaderValidationException()
    {
        // Arrange
        var invalidCsvData = "FirstName,LastName,Email,PhoneNumber\n" + // Missing Header
                             "John,Doe,john@example.com";

        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(invalidCsvData);
        writer.Flush();
        stream.Position = 0;

        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
        fileMock.Setup(f => f.FileName).Returns("invalid_users.csv");
        fileMock.Setup(f => f.Length).Returns(stream.Length);

        var readCsv = new Infrastructure.ReadCsv.ReadCsv();

        // Act & Assert
        Assert.Throws<HeaderValidationException>(() => readCsv.ReadApplicationUsersFromCsv(fileMock.Object));
    }
}

