using LeaderboardService.Application.Commands;
using LeaderboardService.Contracts.Events;
using LeaderboardService.EventHandlers.Interfaces;
using MediatR;
using Serilog;
using Serilog.Context;

namespace LeaderboardService.EventHandlers.Handlers;

public class UpdateUserScoreEventDataHandler : IEventHandler<UpdateUserScoreEventData>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public UpdateUserScoreEventDataHandler(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(UpdateUserScoreEventData eventData)
    {
        _logger.Information("Start processing event {EventName}", typeof(UpdateUserScoreEventData).Name);
        LogContext.PushProperty("EventData", eventData);

        var createOrUpdateCommand =
            new CreateOrUpdateLeaderboardMember(eventData.UserId, eventData.Score, eventData.TimeStamp);

        await _mediator.Send(createOrUpdateCommand);
        
        _logger.Information("Finished processing event {EventName}", typeof(UpdateUserScoreEventData).Name);
    }
}