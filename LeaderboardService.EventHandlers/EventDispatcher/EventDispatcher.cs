using LeaderboardService.Contracts.Events;
using LeaderboardService.EventHandlers.Interfaces;

namespace LeaderboardService.EventHandlers.EventDispatcher;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IDictionary<Type, List<Func<IEventData, Task>>> _eventHandlers =
        new Dictionary<Type, List<Func<IEventData, Task>>>();
    
    public EventDispatcher(IDictionary<Type, List<Func<IEventData, Task>>>  eventHandlers)
    {
        _eventHandlers = eventHandlers;
    }

    public async Task Dispatch<T>(T eventData) 
        where T : IEventData
    {
        var type = eventData.GetType();

        if (_eventHandlers.TryGetValue(type, out var eventHandlersList))
        {
            foreach (var eventHandler in eventHandlersList)
            {
                await eventHandler(eventData);
            }
        }
    }
}