using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.IServices;
using InterviewBallast.Core.Services;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewBallast.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(PlayerRequest playerRequest)
        {
            var player = await _playerService.AddAsync(playerRequest);
            return Created(nameof(AddPlayer), player);
        }

        [HttpGet("GetPlayer")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            var player = await _playerService.GetAsync(playerId);
            return Ok(player);
        }

        [HttpDelete("DeletePlayer")]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            await _playerService.RemoveAsync(playerId);
            return Ok();
        }

        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> UpdatePlayer(PlayerUpdateRequest playerRequest)
        {
            await _playerService.Update(playerRequest);
            return Ok();
        }
    }
}
