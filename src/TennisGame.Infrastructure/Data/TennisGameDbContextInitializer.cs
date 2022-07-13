using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TennisGame.Infrastructure.Data;

public class TennisGameDbContextInitializer
{
    public static async Task InitialiseAsync(TennisGameDbContext context, ILogger logger)
    {
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}