using LeaderboardService.Contracts.Events;

namespace LeaderboardService.EventHandlers.Interfaces;

public interface IEventDispatcher
{
    Task Dispatch<TEventData>(TEventData eventData)
        where TEventData : EventData;
}