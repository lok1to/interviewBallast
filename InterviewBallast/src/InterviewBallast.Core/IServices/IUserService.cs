using InterviewBallast.Core.Dto.Authenticate;
using InterviewBallast.Core.Dto.User;
using InterviewBallast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Core.IServices
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<User?> GetById(int id);
        Task<UserResponse> AddUser(UserRequest userRequest);
    }
}
