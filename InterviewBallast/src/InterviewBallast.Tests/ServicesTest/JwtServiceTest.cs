using InterviewBallast.Common.Helper;
using InterviewBallast.Core.Services;
using InterviewBallast.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace InterviewBallast.Tests.ServicesTest
{
    public class JwtServiceTest
    {
        [Fact]
        public void GenerateJwtToken_ShouldGenerateToken()
        {
            // Arrange
            var appSettings = new AppSettings { Secret = "this is my custom Secret key for authentication" };
            var user = new User { Id = 1 };

            var mockOptions = new Mock<IOptions<AppSettings>>();
            mockOptions.Setup(x => x.Value).Returns(appSettings);

            var jwtService = new JwtService(mockOptions.Object);

            // Act
            var token = jwtService.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.True(token.Length > 0);
        }

        [Fact]
        public void ValidateJwtToken_WithValidToken_ShouldReturnUserId()
        {
            // Arrange
            var appSettings = new AppSettings { Secret = "this is my custom Secret key for authentication" };
            var userId = 1;

            var mockOptions = new Mock<IOptions<AppSettings>>();
            mockOptions.Setup(x => x.Value).Returns(appSettings);

            var jwtService = new JwtService(mockOptions.Object);

            var token = jwtService.GenerateJwtToken(new User { Id = userId });

            // Act
            var result = jwtService.ValidateJwtToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result);
        }

        [Fact]
        public void ValidateJwtToken_WithInvalidToken_ShouldReturnNull()
        {
            // Arrange
            var appSettings = new AppSettings { Secret = "this is my custom Secret key for authentication" };

            var mockOptions = new Mock<IOptions<AppSettings>>();
            mockOptions.Setup(x => x.Value).Returns(appSettings);

            var jwtService = new JwtService(mockOptions.Object);

            // Act
            var result = jwtService.ValidateJwtToken("invalidtoken");

            // Assert
            Assert.Null(result);
        }
    }
}
