using TennisGame.Domain.Entities;

namespace TennisGame.Application.Features.Tournaments.Dtos;

public class PlayerDto
{
    public PlayerType PlayerType { get; set; }
    public string Name { get; set; }
    public int Ability{ get; set; }
    public int? Strength { get; set; }
    public int? Velocity { get; set; }
    public int? ReactionTime { get; set; }

    public static PlayerDto MapFromModel(Player player)
    {
        var dto = new PlayerDto
        {
            PlayerType = player.PlayerType, 
            Name= player.Name, 
            Ability = player.Ability
        };
        
        if (player is MalePlayer malePlayer)
        {
            dto.Strength = malePlayer.Strength;
            dto.Velocity = malePlayer.Velocity;
        }
        
        if(player is FemalePlayer femalePlayer)
        {
            dto.ReactionTime = femalePlayer.ReactionTime;
        }

        return dto;
    }
    
    public static Player ReverseFromDto(PlayerDto dto)
    {
        return dto.PlayerType == PlayerType.Male
            ? new MalePlayer {Name = dto.Name, Ability = dto.Ability, Velocity = dto.Velocity ?? 0, Strength = dto.Strength ?? 0}
            : new FemalePlayer {Name = dto.Name, Ability = dto.Ability, ReactionTime = dto.ReactionTime ?? 0};
    }
};