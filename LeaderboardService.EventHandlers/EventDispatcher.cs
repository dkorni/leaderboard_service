using LeaderboardService.Contracts.Events;
using LeaderboardService.EventHandlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardService.EventHandlers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<TEventData>(TEventData eventData) 
        where TEventData : EventData
    {
        var handler = _serviceProvider.GetService<IEventHandler<TEventData>>();
        if (handler != null)
        {
            await handler.Handle(eventData);
        }
    }
}