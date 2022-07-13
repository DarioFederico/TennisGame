namespace TennisGame.Domain.Entities;

public class MalePlayer : Player
{
    public int Strength { get; set; }
    public int Velocity { get; set; }
    
    public MalePlayer():base(PlayerType.Male)
    {
        
    }

    protected override int GetCustomPoint() => Strength + Velocity;
}