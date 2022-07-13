using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TennisGame.Application.Behaviours;
using TennisGame.Domain.Repositories;
using TennisGame.Infrastructure.Data;
using TennisGame.Infrastructure.Repositories;

namespace TennisGame.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<TennisGameDbContext>(options =>
                options.UseInMemoryDatabase("TennisGameDb"));
        }
        else
        {
            services.AddDbContext<TennisGameDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TennisGameContext")));
        }
        
        
        services.AddTransient<ITournamentRepository, TournamentRepository>();
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(Application.Application));
        return services;
    }

    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Application.Application));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}