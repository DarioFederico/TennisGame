using System;
using System.Collections.Generic;
using AutoFixture;
using TennisGame.Application.Features.Tournaments.Commands;
using TennisGame.Application.Features.Tournaments.Dtos;
using Xunit;

namespace TennisGame.Test.UnitTest.Application.Features.Tournaments;

public class CreateTournamentCommandValidatorTest
{
    private readonly CreateTournamentCommandValidator _sut;
    private readonly Fixture _fixture = new();

    public CreateTournamentCommandValidatorTest()
    {
        _sut = new CreateTournamentCommandValidator();
    }
    
    [Fact]
    public void Validate_ShouldShowPlayersNumbersMessage_WhenZeroPlayersAreSets()
    {
        //Arrange
        var command = _fixture.Build<CreateTournamentCommand>()
            .With(c => c.Name, "Roland Garron")
            .With(c => c.Players, Array.Empty<PlayerDto>())
            .Create();
        
        //Act
        var result = _sut.Validate(command);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Exists(e => e.ErrorMessage == "Player cannot be empty"));
    }
    
    [Fact]
    public void Validate_ShouldShowPlayersTypeMessage_WhenInvalidCountOfPlayersAreSets()
    {
        //Arrange
        var command = _fixture.Build<CreateTournamentCommand>()
            .With(c => c.Name, "Roland Garron")
            .With(c => c.Players, new List<PlayerDto>
            {
                CreatePlayerDto(10)
            })
            .Create();
        
        //Act
        var result = _sut.Validate(command);
        
        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Exists(e => e.ErrorMessage == "The number of players must be a power of 2"));
    }
    
    [Fact]
    public void Validate_ShouldValid_WhenValidParametersAreSet()
    {
        //Arrange
        var command = _fixture.Build<CreateTournamentCommand>()
            .With(c => c.Name, "Roland Garron")
            .With(c => c.Players, new List<PlayerDto>
            {
                CreatePlayerDto(10), CreatePlayerDto(50)
            })
            .Create();
        
        //Act
        var result = _sut.Validate(command);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    private PlayerDto CreatePlayerDto(int ability)
        => _fixture.Build<PlayerDto>()
            .With(p => p.Ability, ability)
            .With(p => p.Name, $"Player with ability {ability}")
            .Create();
}