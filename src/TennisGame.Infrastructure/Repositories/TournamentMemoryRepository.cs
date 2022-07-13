using TennisGame.Domain.Entities;
using TennisGame.Domain.Repositories;

namespace TennisGame.Infrastructure.Repositories;

public class TournamentMemoryRepository: ITournamentRepository
{
    private readonly IDictionary<int, Tournament> _data;

    public TournamentMemoryRepository()
    {
        _data = new Dictionary<int, Tournament>();
    }

    public async Task<IList<Tournament>> GetByCriteria(Criteria criteria)
    {
        return await Task.FromResult(new List<Tournament>());
    }

    public Task<Tournament> AddAsync(Tournament tournament)
    {
        tournament.Id = _data.Count + 1;
        _data.Add(tournament.Id, tournament);
        return Task.FromResult(tournament);
    }
}