using AutoMapper;
using BetSystem.Controllers.Resources;
using BetSystem.Models;

namespace BetSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Team, TeamResource>();
            CreateMap<Match, MatchResource>();
            CreateMap<Bet, BetResource>();
            CreateMap<Season, SeasonResource>();

            // API Resouces to Domain
            CreateMap<TeamResource, Team>();
            CreateMap<MatchResource, Match>();
            CreateMap<BetResource, Bet>();
            CreateMap<SeasonResource, Season>();
        }
    }
}