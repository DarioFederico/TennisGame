using TennisGame.Domain.Entities;

namespace TennisGame.Domain.Repositories;

public interface IPlayerRepository
{
    IList<Player> GetAll();
    Player GetById(int Id);
    Player Add(Player player);
}