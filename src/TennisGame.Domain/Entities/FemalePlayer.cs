namespace TennisGame.Domain.Entities;

public class FemalePlayer : Player
{
    public int ReactionTime { get; set; }
    
    public FemalePlayer() : base(PlayerType.Female)
    {
    }
    
    protected override int GetCustomPoint() => ReactionTime;
}