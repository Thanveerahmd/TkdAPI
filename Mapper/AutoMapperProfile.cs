using AutoMapper;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using TkdAPI.Entities;

namespace TkdScoringApp.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdminDto, Admin>();
            CreateMap<Admin, AdminDto>();

            CreateMap<JudgeDto, Judge>();
            CreateMap<Judge, JudgeDto>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Match, MatchDto>();
            CreateMap<MatchDto, Match>();

            CreateMap<Kickhead, ScoreDto>();
            CreateMap<ScoreDto, Kickhead>();

        }
    }
}