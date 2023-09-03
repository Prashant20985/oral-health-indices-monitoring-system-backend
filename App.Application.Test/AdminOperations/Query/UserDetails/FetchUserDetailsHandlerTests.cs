using App.Application.AdminOperations.Query.UserDetails;
using App.Domain.DTOs;
using App.Domain.Models.Users;
using AutoMapper;
using Moq;

namespace App.Application.Test.AdminOperations.Query.UserDetails;

public class FetchUserDetailsHandlerTests : TestHelper
{
    private readonly IMapper mapper;
    private readonly ApplicationUser applicationUser;
    private readonly FetchUserDetailsHandler handler;
    private readonly FetchUserDetailsQuery query;
    private readonly string UserName;

    public FetchUserDetailsHandlerTests()
    {
        applicationUser = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");
        UserName = "testUser";
        mapper = GetMapper();
        query = new FetchUserDetailsQuery(UserName);
        handler = new FetchUserDetailsHandler(userRepositoryMock.Object, mapper);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResultWithApplicationUser()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(UserName, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result);
        Assert.Equal(applicationUser.UserName, result.ResultValue.UserName);
        Assert.Equal(applicationUser.Email, result.ResultValue.Email);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailureResultWithErrorMessage()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(UserName, CancellationToken.None))
            .ReturnsAsync(value: null);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User not found", result.ErrorMessage);
    }

    private IMapper GetMapper()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, ApplicationUserDto>();
        }).CreateMapper();
    }
}
