namespace TennisGame.Domain.Entities;

public abstract class Player: Entity
{ 
   public PlayerType PlayerType { get; }
   public string Name { get; set; }
   public int Ability { get; set; }

   protected Player(PlayerType playerType)
   {
       PlayerType = playerType;
   }

   public int GeneratePoint() => Ability + GetLuck() + GetCustomPoint();

   protected abstract int GetCustomPoint();

   private int GetLuck() => Ability < 50 ? Ability * 2 : Ability / 2;

   public override bool Equals(object? obj)
   {
       if (obj == this)
           return true;
       
       return obj is Player player && Equals(player);
   }

   private bool Equals(Player other)
   {
       return PlayerType == other.PlayerType && Name == other.Name && Ability == other.Ability;
   }
}