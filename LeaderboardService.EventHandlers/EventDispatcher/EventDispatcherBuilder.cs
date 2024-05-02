using LeaderboardService.Contracts.Events;
using LeaderboardService.EventHandlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardService.EventHandlers;

public class EventDispatcherBuilder
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<Type, List<Func<IEventData, Task>>> _eventHandlers =
        new Dictionary<Type, List<Func<IEventData, Task>>>();

    public EventDispatcherBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public EventDispatcherBuilder AddEventHandler<TEventHandler, TEventData>() 
        where TEventHandler : IEventHandler<TEventData>
        where TEventData : IEventData
    {
        var handler = _serviceProvider.GetRequiredService<TEventHandler>() ;
        var eventType = typeof(TEventData);

        if (_eventHandlers.TryGetValue(eventType, out var list))
        {
            list.Add((x)=>handler.Handle((TEventData)x));
        }
        else
        {
            _eventHandlers[eventType] = new List<Func<IEventData, Task>>()
            {
               (x)=>handler.Handle((TEventData)x)
            };
        }

        return this;
    }

    public EventDispatcher.EventDispatcher Build()
    {
        return new EventDispatcher.EventDispatcher(_eventHandlers);
    }
}