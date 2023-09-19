using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;

namespace App.Application.Test.AdminOperations.Command.CreateApplicationUsersFromCsv;

public class CreateApplicationUsersFromCsvHandlerTests : TestHelper
{
    private readonly List<CreateApplicationUserFromCsvDto> createApplicationUserFromCsvDtos;
    private readonly Mock<IReadCsv> readCsvMock;
    private readonly Mock<IFormFile> fileMock;
    private readonly CreateApplicationUsersFromCsvCommand command;
    private readonly CreateApplicationUsersFromCsvHandler handler;

    public CreateApplicationUsersFromCsvHandlerTests()
    {
        createApplicationUserFromCsvDtos = new List<CreateApplicationUserFromCsvDto>()
        {
             new CreateApplicationUserFromCsvDto
             {
                 FirstName = "Jhon",
                 LastName = "Doe",
                 Email = "test@test.com",
                 PhoneNumber = "1234567890",
                 GuestUserComment = null
             },
             new CreateApplicationUserFromCsvDto
             {
                 FirstName = "Bruce",
                 LastName = "Wayne",
                 Email = "batman@test.com",
                 PhoneNumber = "1234567190",
                 GuestUserComment = null
             }
        };

        readCsvMock = new Mock<IReadCsv>();
        fileMock = new Mock<IFormFile>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CreateApplicationUserFromCsvDto, CreateApplicationUserDto>());
        var mapper = mapperConfig.CreateMapper();
        command = new CreateApplicationUsersFromCsvCommand(fileMock.Object);
        handler = new CreateApplicationUsersFromCsvHandler(mediatorMock.Object, readCsvMock.Object, mapper);
    }

    [Fact]
    public async Task CreateApplicationUsersFromCsv_Success_ReturnsSuccessResult()
    {
        // Arrange
        var csvData = "FirstName,LastName,Email,PhoneNumber,GuestUserComment\nJhon,Doe,test@test.com,1234567890,\nBruce,Wayne,batman@test.com,1234567190,";
        var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvData));

        fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);

        readCsvMock.Setup(csv => csv.ReadApplicationUsersFromCsv(fileMock.Object))
                .Returns(createApplicationUserFromCsvDtos);

        mediatorMock.Setup(m => m.Send(It.IsAny<CreateApplicationUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Contains("Created following users:", result.ResultValue);
        Assert.Contains("Jhon Doe", result.ResultValue);
        Assert.Contains("Bruce Wayne", result.ResultValue);
    }

    [Fact]
    public async Task CreateApplicationUsersFromCsv_Failure_ReturnsFailureResult()
    {
        // Arrange
        var csvData = "FirstName,LastName,Email,PhoneNumber,GuestUserComment\nJhon,Doe,test@test.com,1234567890,\nBruce,Wayne,batman@test.com,1234567190,";
        var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvData));

        fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);

        readCsvMock.Setup(csv => csv.ReadApplicationUsersFromCsv(fileMock.Object))
            .Returns(createApplicationUserFromCsvDtos);

        mediatorMock.Setup(m => m.Send(It.IsAny<CreateApplicationUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Failure("Failed to create user"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Contains("Failed to add the following users:", result.ResultValue);
        Assert.Contains("Jhon Doe", result.ResultValue);
        Assert.Contains("Bruce Wayne", result.ResultValue);
    }

    [Fact]
    public async Task CreateApplicationUsersFromCsv_NoUsersInCsv_ReturnsFailureResult()
    {
        // Arrange
        var csvData = "FirstName,LastName,Email,PhoneNumber,GuestUserComment";
        var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvData));

        fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);

        readCsvMock.Setup(csv => csv.ReadApplicationUsersFromCsv(fileMock.Object))
            .Returns(new List<CreateApplicationUserFromCsvDto>());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("No users found in the CSV file", result.ErrorMessage);
    }
}
