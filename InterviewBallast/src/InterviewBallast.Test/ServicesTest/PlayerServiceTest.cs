using AutoMapper;
using InterviewBallast.Core.Automapper;
using InterviewBallast.Core.IServices;
using InterviewBallast.Core.Services;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using Xunit;

namespace InterviewBallast.Test.ServicesTest
{
    public class PlayerServiceTest
    {
        private readonly Mock<IBaseRepository<Player, InterviewBallastContext>> _mockRepository;
        private IPlayerService _playerService;

        public PlayerServiceTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            var mapper = configuration.CreateMapper();

            _mockRepository = new Mock<IBaseRepository<Player, InterviewBallastContext>>();
            _playerService = new PlayerService(_mockRepository.Object, mapper);
        }

        [Fact]
        public async Task GetPlayerById_WithValidId_ShouldReturnPlayerResponse()
        {
            var player = new Player
            {
                Id = 1,
                FirstName = "Guille",
                LastName = "Yi"
            };

            _mockRepository.Setup(repo => repo.GetAsync(It.Is<Expression<Func<Player, bool>>>(predicate => predicate.Compile().Invoke(player)), default))
                .ReturnsAsync(player);

            var playerResponse = await _playerService.GetAsync(1);

            Assert.Equals(player.Id, playerResponse.Id);
            Assert.Equals(player.FirstName, playerResponse.FirstName);
            Assert.Equals(player.LastName, playerResponse.LastName);
        }
    }
}
