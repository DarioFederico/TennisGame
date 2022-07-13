using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisGame.Domain.Entities;

namespace TennisGame.Infrastructure.Data.Configurations;

public class PlayerConfiguration: IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        
    }
}