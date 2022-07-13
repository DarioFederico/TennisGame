using Microsoft.EntityFrameworkCore;
using TennisGame.Domain.Entities;

namespace TennisGame.Infrastructure.Data;

public class TennisGameDbContext: DbContext
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();
    
    public TennisGameDbContext(DbContextOptions<TennisGameDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<MalePlayer>().HasBaseType<Player>();
        builder.Entity<FemalePlayer>().HasBaseType<Player>();
        builder.Entity<Match>().HasOne<Player>(c => c.PlayerOne).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Match>().HasOne<Player>(c => c.PlayerTwo).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Tournament>().HasMany<Match>().WithOne(m => m.Tournament).OnDelete(DeleteBehavior.NoAction);
        
        builder.ApplyConfigurationsFromAssembly(typeof(TennisGameDbContext).Assembly);
    }
}