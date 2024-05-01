using MediatR;
using Serilog;

namespace LeaderboardService.Application.Behaviors;

public class RequestLoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger _logger;

    public RequestLoggingPipelineBehavior(ILogger logger )
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.Information("Processing request {RequestName}", requestName);
        
        TResponse result = await next();
        
        _logger.Information("Completed request {RequestName}", requestName);

        return result;
    }
}