using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TennisGame.Infrastructure.Data;

public class TennisGameDbContextInitializer
{
    public static async Task SeedAsync(TennisGameDbContext context, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(context, logger, retryForAvailability);
            throw;
        }
    }
}