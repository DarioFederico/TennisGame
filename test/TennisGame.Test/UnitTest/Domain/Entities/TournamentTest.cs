using System;
using System.Linq;
using AutoFixture;
using TennisGame.Domain.Entities;
using Xunit;

namespace TennisGame.Test.UnitTest.Domain.Entities;

public class TournamentTest
{
    private readonly Tournament _sut;
    private readonly Fixture _fixture = new();

    public TournamentTest()
    {
        _sut = new Tournament();
    }
    
    [Fact]
    public void AddMatch_ShouldThrownArgumentException_WhenAddDistinctPlayerType()
    {
        //Arrange
        _sut.AddMatch(CreateMalePlayer(10), CreateMalePlayer(20));

        //Act

        //Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            _sut.AddMatch(CreateFemalePlayer(20), CreateFemalePlayer(35)));
        
        Assert.Equal("Players type are not equals", ex.Message);
    }

    [Fact]
    public void Start_ShouldOneWinner_WhenStartTournament()
    {
        //Arrange
        foreach (var i in Enumerable.Range(1, 256))
        {
            _sut.AddMatch(CreateMalePlayer(i), CreateMalePlayer(i + 2));
        }
        _sut.InitialDate = DateTime.Today;
        
        //Act
        _sut.Start();

        //Assert
        Assert.NotNull(_sut.Winner);
        Assert.Equal("Player with Ability: 258", _sut.Winner.Name);
        Assert.True(_sut.EndDate.CompareTo(_sut.InitialDate) > 0);
    }

    private Player CreateMalePlayer(int ability) =>
        _fixture.Build<MalePlayer>()
            .With(p => p.Name, $"Player with Ability: {ability}")
            .With(p => p.Ability, ability)
            .With(p => p.Strength, ability + 10)
            .With(p => p.Velocity, ability + 2)
            .Create();

    private Player CreateFemalePlayer(int ability) =>
        _fixture.Build<FemalePlayer>()
            .With(p => p.Name, $"Player with Ability: {ability}")
            .With(p => p.Ability, ability)
            .With(p => p.ReactionTime, ability + 4)
            .Create();
}