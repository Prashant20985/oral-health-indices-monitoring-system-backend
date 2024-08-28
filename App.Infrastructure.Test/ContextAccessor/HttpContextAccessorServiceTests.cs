using App.Infrastructure.ContextAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Security.Claims;

namespace App.Infrastructure.Test.ContextAccessor;

public class HttpContextAccessorServiceTests
{
    private readonly Mock<IHttpContextAccessor> httpContextAccessorMock;
    private readonly Mock<HttpRequest> httpRequestMock;
    private readonly Mock<HttpContext> httpContextMock;
    private readonly HttpContextAccessorService httpContextAccessorService;

    public HttpContextAccessorServiceTests()
    {
        httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpRequestMock = new Mock<HttpRequest>();
        httpContextMock = new Mock<HttpContext>();
        httpContextAccessorService = new HttpContextAccessorService(httpContextAccessorMock.Object);
    }

    [Fact]
    public void GetOrigin_ShouldReturnOriginFromRequestHeaders()
    {
        // Arrange
        var originHeaderValue = "https://example.com";

        httpContextMock.Setup(h => h.Request).Returns(httpRequestMock.Object);

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        httpRequestMock.Setup(r => r.Headers["origin"])
            .Returns(new StringValues(originHeaderValue));

        // Act
        var origin = httpContextAccessorService.GetOrigin();

        // Assert
        Assert.Equal(originHeaderValue, origin);
    }

    [Fact]
    public void GetOrigin_ShouldReturnNull()
    {
        // Arrange
        var httpRequestMock = new Mock<HttpRequest>();

        httpContextMock.Setup(h => h.Request).Returns(httpRequestMock.Object);

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        httpRequestMock.Setup(r => r.Headers["origin"])
            .Returns(new StringValues());

        // Act
        var result = httpContextAccessorService.GetOrigin();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUserName_ShouldReturnUserNameFromHttpContext()
    {
        // Arrange
        var userName = "testuser";
        var userMock = new Mock<ClaimsPrincipal>();

        userMock.Setup(u => u.Identity).Returns(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, userName)
        }));

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        httpContextMock.Setup(u => u.User)
            .Returns(userMock.Object);

        // Act
        var result = httpContextAccessorService.GetUserName();

        // Assert
        Assert.Equal(userName, result);
    }

    [Fact]
    public void GetUserName_ShouldReturnNull_WhenHttpContextIsNull()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.SetupGet(a => a.HttpContext)
            .Returns(value: null);

        // Act
        var result = httpContextAccessorService.GetUserName();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetRefreshTokenCookie_ShouldReturnRefreshTokenCookieValue()
    {
        // Arrange
        var refreshTokenCookieValue = "my-refresh-token";

        httpRequestMock.Setup(r => r.Cookies["refreshToken"])
            .Returns(refreshTokenCookieValue);

        httpContextMock.Setup(u => u.Request)
            .Returns(httpRequestMock.Object);

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        // Act
        var result = httpContextAccessorService.GetRefreshTokenCookie();

        // Assert
        Assert.Equal(refreshTokenCookieValue, result);
    }

    [Fact]
    public void GetRefreshTokenCookie_ShouldReturnNull()
    {
        // Arrange
        httpRequestMock.Setup(r => r.Cookies["refreshToken"])
            .Returns(value: null);

        httpContextMock.Setup(u => u.Request)
            .Returns(httpRequestMock.Object);

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        // Act
        var result = httpContextAccessorService.GetRefreshTokenCookie();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetResponse_ShouldReturnHttpResponseFromHttpContext()
    {
        // Arrange
        var httpResponseMock = new Mock<HttpResponse>();

        httpContextMock.Setup(r => r.Response).Returns(httpResponseMock.Object);

        httpContextAccessorMock.Setup(a => a.HttpContext)
            .Returns(httpContextMock.Object);

        // Act
        var result = httpContextAccessorService.GetResponse();

        // Assert
        Assert.Same(httpResponseMock.Object, result);
    }

    [Fact]
    public void GetResponse_ShouldReturnNull_WhenHttpContextIsNull()
    {
        // Arrange
        httpContextAccessorMock.SetupGet(a => a.HttpContext)
            .Returns(value: null);

        // Act
        var result = httpContextAccessorService.GetResponse();

        // Assert
        Assert.Null(result);
    }
}
