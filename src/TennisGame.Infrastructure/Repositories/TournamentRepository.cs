using Microsoft.EntityFrameworkCore;
using TennisGame.Domain.Entities;
using TennisGame.Domain.Repositories;
using TennisGame.Infrastructure.Data;

namespace TennisGame.Infrastructure.Repositories;

public class TournamentRepository: ITournamentRepository
{
    private readonly TennisGameDbContext _context;

    public TournamentRepository(TennisGameDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Tournament>> GetByCriteria(Criteria criteria)
    {
        var query = (Tournament t) => true;

        if (criteria.InitialDate.HasValue)
        {
            query += tournament => tournament.InitialDate >= criteria.InitialDate;
        }
        
        if (criteria.PlayerType.HasValue)
        {
            query += tournament => tournament.PlayerType == criteria.PlayerType;
        }

        var result = _context.Tournaments
            .Include(m => m.Winner)
            .Include(t => t.Matches)
            .ThenInclude(m => m.PlayerOne)
            .Where(query)
            .Skip(criteria.Offset)
            .Take(criteria.Size)
            .ToList();
        
        return await Task.FromResult(result);
    }

    public async Task<Tournament> AddAsync(Tournament tournament)
    {
        await _context.Tournaments.AddAsync(tournament);
        await _context.SaveChangesAsync();
        return tournament;
    }
}