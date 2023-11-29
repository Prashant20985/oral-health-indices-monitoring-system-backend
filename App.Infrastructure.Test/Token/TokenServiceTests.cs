using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Infrastructure.Configuration;
using App.Infrastructure.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Infrastructure.Test.Token
{
    public class TokenServiceTests
    {
        private readonly JwtConfig jwtConfig;
        private readonly Mock<RoleManager<ApplicationRole>> roleManagerMock;
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        private readonly Mock<IHttpContextAccessorService> httpContextAccessorServiceMock;
        private readonly Mock<IOptionsMonitor<JwtConfig>> jwtConfigOptionsMonitor;
        private readonly TokenService tokenService;

        public TokenServiceTests()
        {
            jwtConfig = new JwtConfig { SecretKey = "yourSuperSecretKeyWith128BitsOrMore", AccessTokenExpiration = 60 };

            jwtConfigOptionsMonitor = new Mock<IOptionsMonitor<JwtConfig>>();
            jwtConfigOptionsMonitor.Setup(opt => opt.CurrentValue).Returns(jwtConfig);

            userManagerMock = GetUserManagerMock();
            roleManagerMock = GetRoleManagerMock();
            httpContextAccessorServiceMock = new Mock<IHttpContextAccessorService>();

            tokenService = new TokenService(
                jwtConfigOptionsMonitor.Object,
                roleManagerMock.Object,
                userManagerMock.Object,
                httpContextAccessorServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateToken_ReturnsValidToken()
        {
            // Arrange
            var user = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");

            userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string>() { "Admin" });

            roleManagerMock.Setup(x => x.FindByNameAsync("Admin"))
                .ReturnsAsync(new ApplicationRole { Name = "Admin" });

            // Act
            var token = await tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.True(token.Length > 0);
            Assert.Contains(".", token);
        }

        private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            Mock<UserManager<ApplicationUser>> userManagerMock = new(storeMock.Object, null, null, null, null, null, null, null, null);

            return userManagerMock;
        }

        private static Mock<RoleManager<ApplicationRole>> GetRoleManagerMock()
        {
            var storeMock = new Mock<IRoleStore<ApplicationRole>>();
            Mock<RoleManager<ApplicationRole>> roleManagerMock = new(storeMock.Object, null, null, null, null);

            return roleManagerMock;
        }

        [Fact]
        public async Task GetAllUserClaims_ReturnsCorrectClaims()
        {
            // Arrange
            var user = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");

            userManagerMock.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin", "User" });

            roleManagerMock.Setup(rm => rm.FindByNameAsync("Admin"))
                .ReturnsAsync(new ApplicationRole { Name = "Admin" });
            roleManagerMock.Setup(rm => rm.FindByNameAsync("User"))
                .ReturnsAsync(new ApplicationRole { Name = "User" });

            // Act
            var claims = await tokenService.GetAllUserClaims(user);

            // Assert
            Assert.NotNull(claims);
            Assert.NotEmpty(claims);
            Assert.Contains(claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id);
            Assert.Contains(claims, c => c.Type == ClaimTypes.Name && c.Value == user.UserName);
            Assert.Contains(claims, c => c.Type == JwtRegisteredClaimNames.Jti);
            Assert.Contains(claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            Assert.Contains(claims, c => c.Type == ClaimTypes.Role && c.Value == "User");
        }

        [Fact]
        public async Task SetsRefreshTokenAndCookie()
        {
            // Arrange
            var user = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");

            userManagerMock.Setup(um => um.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var httpResponse = new DefaultHttpContext().Response;
            httpContextAccessorServiceMock.Setup(a => a.GetResponse()).Returns(httpResponse);

            // Act
            await tokenService.SetRefreshToken(user);

            // Assert
            userManagerMock.Verify(um => um.UpdateAsync(user), Times.Once);

            Assert.NotNull(httpResponse.Body);
        }
    }
}
