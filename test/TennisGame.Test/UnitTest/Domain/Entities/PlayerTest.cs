using TennisGame.Domain.Entities;
using Xunit;

namespace TennisGame.Test.UnitTest.Domain.Entities;

public class PlayerTest
{
    private Player _sut;

    [Fact]
    public void Create_ShouldBeMenType_WhenMenPlayerIsCreated()
    {
        //Arrange
        
        //Act
        _sut = new MalePlayer();

        //Assert
        Assert.Equal(PlayerType.Male, _sut.PlayerType);
    }
    
    [Fact]
    public void Create_ShouldBeFemaleType_WhenFemalePlayerIsCreated()
    {
        //Arrange
        
        //Act
        _sut = new FemalePlayer();
        
        //Assert
        Assert.Equal(PlayerType.Female, _sut.PlayerType);
    }
    
    [Fact]
    public void GetMalePoint_ShouldZero_WhenAllParamsAreZero()
    {
        //Arrange
        _sut = new MalePlayer
        {
            Name = "Dario",
            Ability = 0,
            Strength = 0,
            Velocity = 0
        };
        
        //Act
        var point = _sut.GeneratePoint();
        
        //Assert
        Assert.Equal(0, point);

    }
    
    [Fact]
    public void GetFemalePoint_ShouldZero_WhenAllParamsAreZero()
    {
        //Arrange
        _sut = new FemalePlayer
        {
            Name = "Jane",
            Ability = 0,
            ReactionTime = 0
        };
        
        //Act
        var point = _sut.GeneratePoint();
        
        //Assert
        Assert.Equal(0, point);
    }
    
    [Fact]
    public void GetMalePoint_ShouldNotZero_WhenAllParamsNotZero()
    {
        //Arrange
        _sut = new MalePlayer
        {
            Name = "Dario",
            Ability = 10,
            Strength = 5,
            Velocity = 5
        };
        
        //Act
        var point = _sut.GeneratePoint();
        
        //Assert
        Assert.Equal(40, point);
    }
    
    [Fact]
    public void GetFemalePoint_ShouldNotZero_WhenAllParamsNotZero()
    {
        //Arrange
        _sut = new FemalePlayer
        {
            Name = "Jane",
            Ability = 10,
            ReactionTime = 50
        };
        
        //Act
        var point = _sut.GeneratePoint();
        
        //Assert
        Assert.Equal(80, point);
    }
}