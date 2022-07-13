using System;
using AutoFixture;
using NSubstitute;
using TennisGame.Domain.Entities;
using Xunit;

namespace TennisGame.Test.UnitTest.Domain.Entities;

public class MatchTest
{
    private readonly Match _sut;
    private readonly Tournament _tournament = Substitute.For<Tournament>();
    private readonly IFixture _fixture = new Fixture();

    public MatchTest()
    {
        _sut = new Match(_tournament);
    }

    [Fact]
    public void AddPlayers_ShouldValid_WhenParamsAreValid()
    {
        //Arrange
        Player playerOne = new MalePlayer();
        Player playerTwo = new MalePlayer();
        
        //Acts
        Exception? exception = null;
        try
        {
            _sut.AddPlayers(playerOne, playerTwo);
        }
        catch (Exception e)
        {
            exception = e;
        }

        
        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void PlayMatch_ShouldOneWinner_WhenTwoPlayersAreSetter()
    {
        //Arrange
        Player playerOne = _fixture.Build<FemalePlayer>()
            .With(p => p.Name, "Jane")
            .With(p => p.Ability, 10)
            .With(p => p.ReactionTime, 10)
            .Create();
        Player playerTwo = _fixture.Build<FemalePlayer>()
            .With(p => p.Name, "Maria")
            .With(p => p.Ability, 20)
            .With(p => p.ReactionTime, 15)
            .Create();
        
        //Acts
        _sut.AddPlayers(playerOne, playerTwo);
        _sut.PlayMatch();

        //Assert
        Assert.Equal(playerTwo, _sut.Winner);
    }
}