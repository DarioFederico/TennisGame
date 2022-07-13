using MediatR;
using Microsoft.Extensions.Logging;

namespace TennisGame.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var requestId = Guid.NewGuid();
        //Request
        _logger.LogInformation($"Initize request {requestId} : Handling {requestName}");

        var response = await next();
        
        //Response
        _logger.LogInformation($"Finished request {requestId} : Handled {requestName}");
        
        return response;
    }
}