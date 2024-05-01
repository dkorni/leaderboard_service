using LeaderboardService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Serilog.ILogger;

namespace LeaderboardService.API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private ILogger _logger;
    
    public GlobalExceptionFilter(ILogger logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            _logger.Warning(validationException,"Completed request with validation error: {Error}", validationException.Message);
            
            var errorObject = new
            {
                ErrorMessage = validationException.Message
            };
            
            context.Result = new BadRequestObjectResult(errorObject);
        }
        else
        {
            _logger.Error(context.Exception, "Completed request with internal server error");
            
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}