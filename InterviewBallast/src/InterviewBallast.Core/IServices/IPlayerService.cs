using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Core.IServices
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerResponse>> GetAllAsync();
        Task<PlayerResponse> AddAsync(PlayerRequest playerRequest);
        Task<PlayerResponse> GetAsync(int playerId);
        Task RemoveAsync(int playerId);
        Task Update(PlayerUpdateRequest playerRequest);
    }
}
