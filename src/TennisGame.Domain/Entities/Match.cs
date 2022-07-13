namespace TennisGame.Domain.Entities;

public class Match: Entity
{
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public Player Winner { get; set; }
    
    public Tournament Tournament { get; private set; }
    public bool HasFinish { get; private set; }

    public Match() { }

    public Match(Tournament tournament)
    {
        Tournament = tournament;
        HasFinish = false;
    }

    public void AddPlayers(Player playerOne, Player playerTwo)
    {
        if (playerOne == null) throw new ArgumentNullException(nameof(playerOne));
        if (playerTwo == null) throw new ArgumentNullException(nameof(playerTwo));

        if (playerOne.PlayerType != playerTwo.PlayerType)
            throw new ArgumentException("Players type are not equals");

        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }

    public void PlayMatch()
    {
        var playerOnePoint = PlayerOne.GeneratePoint();
        var playerTwoPoint = PlayerTwo.GeneratePoint();
        Winner = playerOnePoint > playerTwoPoint ? PlayerOne : PlayerTwo;
        HasFinish = true;
    }
}