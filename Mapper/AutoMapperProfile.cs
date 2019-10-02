using AutoMapper;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<Judge, JudgeDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Match, MatchDto>();

        }
    }
}