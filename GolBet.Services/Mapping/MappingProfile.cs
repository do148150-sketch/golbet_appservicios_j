// GolBet.Services/Mapping/MappingProfile.cs 

using AutoMapper;

using GolBet.Entities;

using GolBet.Services.DTOs;



namespace GolBet.Services.Mapping;



public class MappingProfile : Profile

{

    public MappingProfile()

    {

        // Flattening by convention: 

        // MatchDto.HomeTeamName  <- Match.HomeTeam.Name 

        // MatchDto.AwayTeamCrestUrl <- Match.AwayTeam.CrestUrl 

        CreateMap<Match, MatchDto>();

        // GolBet.Services/Mapping/MappingProfile.cs  (agregar dentro del constructor) 

        CreateMap<Match, MatchDetailDto>()

            .ForMember(dto => dto.TotalBets,

                       options => options.MapFrom(match => match.Bets.Count));

        CreateMap<Team, TeamDto>();

        CreateMap<TeamFormDto, Team>().ReverseMap();

        CreateMap<MatchFormDto, Match>().ReverseMap();

        // Two-hop flattening: BetDto.MatchHomeTeamName <- Bet.Match.HomeTeam.Name

        // PotentialPayout is a CALCULATION, not a path: convention cannot resolve it

        CreateMap<Bet, BetDto>()

            .ForMember(dto => dto.PotentialPayout,

                       options => options.MapFrom(bet => bet.Amount * bet.OddsAtPlacement));


    }

}