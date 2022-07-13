using MediatR;
using TennisGame.Application.Features.Tournaments.Dtos;
using TennisGame.Domain.Entities;
using TennisGame.Domain.Repositories;

namespace TennisGame.Application.Features.Tournaments.Commands;

public record CreateTournamentCommand(string Name, DateTime InitialDate, PlayerType PlayerType,
    IEnumerable<PlayerDto> Players) : IRequest<CreateTournamentDto>;
    
public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, CreateTournamentDto>
{
    private readonly ITournamentRepository _tournamentRepository;


    public CreateTournamentCommandHandler(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<CreateTournamentDto> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        var tournament = Tournament.Create(request.Name, request.PlayerType, request.InitialDate);

        var playerChunks = request.Players.Chunk(2);

        foreach (var playerChunk in playerChunks)
        {
            tournament.AddMatch(PlayerDto.ReverseFromDto(playerChunk[0]), PlayerDto.ReverseFromDto(playerChunk[1]));
        }
        
        tournament.Start();

        await _tournamentRepository.AddAsync(tournament);
        
        return CreateTournamentDto.MapToDto(tournament);
    }
}