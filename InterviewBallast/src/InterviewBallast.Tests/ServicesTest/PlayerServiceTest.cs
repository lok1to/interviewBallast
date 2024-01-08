using AutoMapper;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.IServices;
using InterviewBallast.Core.Services;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace InterviewBallast.Tests.ServicesTest
{
    public class PlayerServiceTest
    {
        private readonly Mock<IBaseRepository<Player, InterviewBallastContext>> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private IPlayerService _playerService;

        public PlayerServiceTest()
        {
            _mockMapper = new Mock<IMapper>();

            _mockRepository = new Mock<IBaseRepository<Player, InterviewBallastContext>>();
            _playerService = new PlayerService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPlayer()
        {
            // Arrange
            var playerRequest = new PlayerRequest { FirstName = "Juan", LastName = "Perez"};
            var player = new Player { Id = 2, FirstName = "Juan", LastName = "Perez", Active = true, Created = DateTime.Today };
            var playerResponse = new PlayerResponse { FirstName = "Juan", Id = 2 , Active = true, Created = DateTime.Today };
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()));
            _mockMapper.Setup(mapper => mapper.Map<Player>(playerRequest)).Returns(player);
            _mockMapper.Setup(mapper => mapper.Map<PlayerResponse>(player)).Returns(playerResponse);

            // Act
            var result = await _playerService.AddAsync(playerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerResponse, result);
        }

        [Fact]
        public async Task GetPlayerById_WithValidId_ShouldReturnPlayerResponse()
        {
            // Arrange
            var player = new Player
            {
                Id = 1,
                FirstName = "Guille",
                LastName = "Yi"
            };

            _mockRepository.Setup(repo => repo.GetAsync(It.Is<Expression<Func<Player, bool>>>(predicate => predicate.Compile().Invoke(player)), default))
                .ReturnsAsync(player);

            // Act
            var playerResponse = await _playerService.GetAsync(1);

            // Assert
            Assert.Equal(player.Id, playerResponse.Id);
            Assert.Equal(player.FirstName, playerResponse.FirstName);
            Assert.Equal(player.LastName, playerResponse.LastName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPlayerResponses()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Id = 1, FirstName = "John", LastName = "Doe" },
                new Player { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            var playerResponses = players.Select(player => new PlayerResponse
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName
            }).ToList();

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(players);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PlayerResponse>>(players)).Returns(playerResponses);

            // Act
            var result = await _playerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerResponses.Count, result.Count());
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemovePlayer()
        {
            // Arrange
            var playerId = 1;

            // Act
            await _playerService.RemoveAsync(playerId);

            // Assert
            _mockRepository.Verify(repo => repo.Remove(playerId), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdatePlayer()
        {
            // Arrange
            var playerRequest = new PlayerUpdateRequest
            {
                Id = 1,
                FirstName = "Julian",
                LastName = "Rodriguez"
            };

            var updatedPlayer = new Player
            {
                Id = 1,
                FirstName = "Julian",
                LastName = "Rodriguez"
            };

            _mockMapper.Setup(mapper => mapper.Map<Player>(playerRequest)).Returns(updatedPlayer);

            // Act
            await _playerService.Update(playerRequest);

            // Assert
            _mockMapper.Verify(mapper => mapper.Map<Player>(playerRequest), Times.Once);
            _mockRepository.Verify(repo => repo.Update(updatedPlayer), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
