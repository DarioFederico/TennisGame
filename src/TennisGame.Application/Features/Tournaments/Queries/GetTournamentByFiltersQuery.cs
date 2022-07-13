using MediatR;
using TennisGame.Application.Features.Tournaments.Commands;
using TennisGame.Application.Features.Tournaments.Dtos;
using TennisGame.Domain.Entities;
using TennisGame.Domain.Repositories;

namespace TennisGame.Application.Features.Tournaments.Queries;

public record GetTournamentByFiltersQuery
    (DateTime? initialDate, PlayerType? playerType, int page = 1, int size = 10) : IRequest<IEnumerable<TournamentDto>>;

public record MatchDto(PlayerDto PlayerOne, PlayerDto PlayerTwo, bool HasFinished);

public record TournamentDto(int Id, string Name, DateTime InitialDate, DateTime EndDate, IList<MatchDto> Matches, PlayerDto Winner)
{
    public static TournamentDto MapFromModel(Tournament model) =>
        new
        (
            model.Id,
            model.Name,
            model.InitialDate,
            model.EndDate,
            model.Matches.Select(m =>
                    new MatchDto(PlayerDto.MapFromModel(m.PlayerOne), PlayerDto.MapFromModel(m.PlayerOne), m.HasFinish))
                .ToList(),
            PlayerDto.MapFromModel(model.Winner)
        );
}

public class GetTournamentByFiltersQueryHandler : IRequestHandler<GetTournamentByFiltersQuery, IEnumerable<TournamentDto>>
{
    private readonly ITournamentRepository _tournamentRepository;

    public GetTournamentByFiltersQueryHandler(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<IEnumerable<TournamentDto>> Handle(GetTournamentByFiltersQuery request, CancellationToken cancellationToken)
    {
        var criteria = new Criteria(request.initialDate,
            request.playerType, request.page,
            request.size);

        var tournament =
            await _tournamentRepository.GetByCriteria(criteria);

        return tournament.Select(TournamentDto.MapFromModel);
    }
}