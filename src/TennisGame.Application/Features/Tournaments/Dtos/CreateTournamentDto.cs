using TennisGame.Domain.Entities;

namespace TennisGame.Application.Features.Tournaments.Dtos;

public record CreateTournamentDto(PlayerDto Winner)
{
    public static CreateTournamentDto MapToDto(Tournament model)
        => new (PlayerDto.MapFromModel(model.Winner));
}