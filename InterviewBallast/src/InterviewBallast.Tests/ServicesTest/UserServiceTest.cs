using AutoMapper;
using InterviewBallast.Core.Dto.Authenticate;
using InterviewBallast.Core.Dto.User;
using InterviewBallast.Core.IServices;
using InterviewBallast.Core.Services;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;
using Moq;
using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;

namespace InterviewBallast.Tests.ServicesTest
{
    public class UserServiceTests
    {
        private readonly Mock<IBaseRepository<User, InterviewBallastAuthContext>> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IJwtService> _mockJwtService;
        private IUserService _mockUserService;

        public UserServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IBaseRepository<User, InterviewBallastAuthContext>>();
            _mockJwtService = new Mock<IJwtService>();
            _mockUserService = new UserService(_mockRepository.Object, _mockJwtService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Authenticate_WithValidCredentials_ShouldReturnAuthenticateResponse()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "testuser",
                Password = "testpassword"
            };

            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Password = "testpassword"
            };

            var authenticateResponse = new AuthenticateResponse(user, "testtoken");

            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default))
                          .ReturnsAsync(user);
            _mockJwtService.Setup(jwtUtils => jwtUtils.GenerateJwtToken(user))
                        .Returns("testtoken");

            // Act
            var result = await _mockUserService.Authenticate(authenticateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(authenticateResponse.Token, result.Token);
            Assert.Equal(authenticateResponse.Id, result.Id);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                Id = userId,
            };

            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), default))
                          .ReturnsAsync(user);

            // Act
            var result = await _mockUserService.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task AddUser_WithValidUserRequest_ShouldReturnUserResponse()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                Username = "Test",
                Password = "TestPassword"
            };

            var user = new User
            {
                Id = 1,
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
            };

            var userResponse = new UserResponse
            {
                Id=1,
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Username = "Test",
                Password = "TestPassword"
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), default));

            _mockMapper.Setup(mapper => mapper.Map<User>(userRequest))
                      .Returns(user);
            _mockMapper.Setup(mapper => mapper.Map<UserResponse>(user))
                      .Returns(userResponse);

            // Act
            var result = await _mockUserService.AddUser(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userResponse.Id, result.Id);
        }
    }
}
