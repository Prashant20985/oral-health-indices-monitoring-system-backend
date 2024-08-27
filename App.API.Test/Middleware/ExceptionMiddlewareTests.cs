using System.Net;
using System.Text.Json;
using App.API.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.API.Test.Middleware;

public class ExceptionMiddlewareTests
{
    private readonly DefaultHttpContext httpContext;
    private readonly Mock<ILogger<ExceptionMiddleware>> loggerMock;
    private readonly MemoryStream responseStream;

    public ExceptionMiddlewareTests()
    {
        httpContext = new DefaultHttpContext();
        responseStream = new MemoryStream();
        httpContext.Response.Body = responseStream;
        loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
    }

    [Fact]
    public async Task InvokeAsync_ExceptionThrown_ShouldHandleException()
    {
        // Arrange
        var exceptionMessage = "An error occurred.";
        var nextMiddleware = new RequestDelegate(context => throw new Exception(exceptionMessage));

        var middleware = new ExceptionMiddleware(nextMiddleware, loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Ensure that the response stream is flushed
        responseStream.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(responseStream);
        var responseBody = await reader.ReadToEndAsync();

        // Deserialize the JSON response
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var exceptionDetails = JsonSerializer.Deserialize<AppException>(responseBody, options);

        // Assert
        Assert.NotNull(exceptionDetails);
        Assert.Equal((int)HttpStatusCode.InternalServerError, httpContext.Response.StatusCode);
        Assert.Equal(exceptionMessage, exceptionDetails.Message);

        // Verify that the logger recorded the exception
        loggerMock.VerifyLog(
            x => x.LogError(
                It.IsAny<Exception>(),
                It.Is<string>(s => s == exceptionMessage)),
            Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_NoExceptionThrown_ShouldNotHandleException()
    {
        // Arrange
        var nextMiddleware = new RequestDelegate(context => Task.CompletedTask);

        var middleware = new ExceptionMiddleware(nextMiddleware, loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Ensure that the response stream is flushed
        responseStream.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(responseStream);
        var responseBody = await reader.ReadToEndAsync();

        // Assert
        Assert.Empty(responseBody);
        Assert.Equal((int)HttpStatusCode.OK, httpContext.Response.StatusCode);

        // Verify that the logger did not record any exceptions
        loggerMock.VerifyLog(
            x => x.LogError(
                It.IsAny<Exception>(),
                It.IsAny<string>()),
            Times.Never);
    }
}