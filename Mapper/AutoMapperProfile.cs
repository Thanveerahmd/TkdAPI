using AutoMapper;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using TkdAPI.Entities;
using TkdAPI.Dto;

namespace TkdScoringApp.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdminDto, Admin>();
            CreateMap<Admin, AdminDto>();

            CreateMap<AdminTokenReturnDto, Admin>();
            CreateMap<Admin, AdminTokenReturnDto>();

            CreateMap<FoulDto, Foul>();
            CreateMap<Foul, FoulDto>();

            CreateMap<JudgeDto, Judge>();
            CreateMap<Judge, JudgeDto>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Match, MatchDto>();
            CreateMap<MatchDto, Match>();

            CreateMap<Kickhead, ScoreDto>();
            CreateMap<ScoreDto, Kickhead>();

            CreateMap<KickBody, ScoreDto>();
            CreateMap<ScoreDto, KickBody>();

            CreateMap<Punch, ScoreDto>();
            CreateMap<ScoreDto, Punch>();

            CreateMap<TurningKickBody, ScoreDto>();
            CreateMap<ScoreDto, TurningKickBody>();

            CreateMap<TurningKickHead, ScoreDto>();
            CreateMap<ScoreDto, TurningKickHead>();

            CreateMap<ScoreDto,Score>();
            CreateMap<Score,ScoreDto>();

        }
    }
}