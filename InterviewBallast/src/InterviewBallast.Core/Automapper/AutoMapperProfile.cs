using AutoMapper;
using InterviewBallast.Core.Dto.Address;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.Dto.User;
using InterviewBallast.Domain.Entities;

namespace InterviewBallast.Core.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, PlayerRequest>().ReverseMap();
            CreateMap<Player, PlayerResponse>().ReverseMap();
            CreateMap<Address, AddressRequest>().ReverseMap();
            CreateMap<Address, AddressResponse>().ReverseMap();
            CreateMap<Player, PlayerUpdateRequest>().ReverseMap();
            CreateMap<Address, AddressUpdateRequest>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
