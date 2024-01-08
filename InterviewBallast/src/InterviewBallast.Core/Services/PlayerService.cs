using AutoMapper;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.IServices;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;

namespace InterviewBallast.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IBaseRepository<Player, InterviewBallastContext> _repository;
        private readonly IMapper _mapper;

        public PlayerService(IBaseRepository<Player, InterviewBallastContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlayerResponse> AddAsync(PlayerRequest playerRequest)
        {
            var player = _mapper.Map<Player>(playerRequest);
            
            await _repository.AddAsync(player);
            await _repository.SaveChangesAsync();

            return _mapper.Map<PlayerResponse>(player);
        }

        public async Task<IEnumerable<PlayerResponse>> GetAllAsync()
        {
            var players = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlayerResponse>>(players);
        }

        public async Task<PlayerResponse> GetAsync(int playerId)
        {
            var player = await _repository.GetAsync(x => x.Id == playerId);
            return _mapper.Map<PlayerResponse>(player);
        }

        public async Task RemoveAsync(int playerId)
        {
            await _repository.Remove(playerId);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(PlayerUpdateRequest playerRequest)
        {
            var player = _mapper.Map<Player>(playerRequest);
            await _repository.Update(player);
            await _repository.SaveChangesAsync();
        }
    }
}
