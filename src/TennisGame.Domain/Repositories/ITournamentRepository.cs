using TennisGame.Domain.Entities;

namespace TennisGame.Domain.Repositories;

public interface ITournamentRepository
{
    Task<IList<Tournament>> GetByCriteria(Criteria criteria);
    Task<Tournament> AddAsync(Tournament tournament);
}

public record Criteria(DateTime? InitialDate, PlayerType? PlayerType, int Page, int Size)
{
    public int Offset => (Page - 1) * Size;
}