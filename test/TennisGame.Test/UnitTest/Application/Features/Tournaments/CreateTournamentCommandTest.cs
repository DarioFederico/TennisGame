using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using NSubstitute;
using TennisGame.Application.Features.Tournaments.Commands;
using TennisGame.Application.Features.Tournaments.Dtos;
using TennisGame.Domain.Entities;
using TennisGame.Domain.Repositories;
using Xunit;

namespace TennisGame.Test.UnitTest.Application.Features.Tournaments;

public class CreateTournamentCommandTest
{
    private readonly CreateTournamentCommandHandler _sut;
    private readonly ITournamentRepository _tournamentRepository = Substitute.For<ITournamentRepository>();
    private readonly Fixture _fixture = new();

    public CreateTournamentCommandTest()
    {
        _sut = new CreateTournamentCommandHandler(_tournamentRepository);
    }

    [Fact]
    public async void CreateTournament_ShouldReturnWinner_WhenDtoAreValid()
    {
        //Arrange
        var command = new CreateTournamentCommand("Roland Garron", DateTime.Today, PlayerType.Male,
            CreatePlayersDto(8));
        
        var tournament = _fixture.Build<Tournament>()
            .With(t => t.Name, command.Name)
            .With(t => t.PlayerType, command.PlayerType)
            .With(t => t.InitialDate, command.InitialDate)
            .Without(p => p.Winner)
            .Create();

        _tournamentRepository.AddAsync(tournament).Returns(tournament);

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Player name: 8", result.Winner.Name);
    }

    private IEnumerable<PlayerDto> CreatePlayersDto(int nroPlayers) =>
        Enumerable.Range(1, nroPlayers).Select(i =>
            _fixture.Build<PlayerDto>()
                .With(p => p.PlayerType, PlayerType.Male)
                .With(p => p.Name, $"Player name: {i}")
                .With(p => p.Ability, i)
                .With(p => p.Velocity, i)
                .With(p => p.Strength, i)
                .Create());
}