using AutoMapper;
using InterviewBallast.Core.Dto.Authenticate;
using InterviewBallast.Core.Dto.User;
using InterviewBallast.Core.IServices;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;

namespace InterviewBallast.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, InterviewBallastAuthContext> _userRepository;
        private readonly IJwtService _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User, InterviewBallastAuthContext> repository, IJwtService jwtUtils, IMapper mapper)
        {
            _userRepository = repository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = await _userRepository.GetAsync(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetAsync(x => x.Id == id);
        }

        public async Task<UserResponse> AddUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }
    }
}
