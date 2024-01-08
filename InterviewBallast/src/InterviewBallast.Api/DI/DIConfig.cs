using FluentValidation;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.IServices;
using InterviewBallast.Core.Services;
using InterviewBallast.Core.Validators;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;
using InterviewBallast.Infrastructure.Repositories;

namespace InterviewBallast.Api.DI
{
    public class DIConfig
    {
        protected DIConfig() { }

        public static IServiceCollection LoadServices(IServiceCollection services)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IBaseRepository<Player, InterviewBallastContext>, BaseRepository<Player, InterviewBallastContext>>();
            services.AddScoped<IBaseRepository<User, InterviewBallastAuthContext>, BaseRepository<User, InterviewBallastAuthContext>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IValidator<PlayerRequest>, PlayerValidator>();

            return services;
        }
    }
}
