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
        var tournamentFilter = _context.Tournaments.Where(t => true);
        
        if (criteria.InitialDate.HasValue)
        {
            tournamentFilter = tournamentFilter.Where(t => t.InitialDate >= criteria.InitialDate);
        }
        
        if (criteria.PlayerType.HasValue)
        {
            tournamentFilter = tournamentFilter.Where(t => t.PlayerType == criteria.PlayerType);
        }

        return await tournamentFilter.Include(m => m.Winner)
            .Include(t => t.Matches)
            .ThenInclude(m => m.PlayerOne)
            .Skip(criteria.Offset)
            .Take(criteria.Size)
            .ToListAsync();
    }

    public async Task<Tournament> AddAsync(Tournament tournament)
    {
        await _context.Tournaments.AddAsync(tournament);
        await _context.SaveChangesAsync();
        return tournament;
    }
}
