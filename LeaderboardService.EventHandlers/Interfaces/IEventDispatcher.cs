using LeaderboardService.Contracts.Events;

namespace LeaderboardService.EventHandlers.Interfaces;

public interface IEventDispatcher
{
    Task Dispatch<T>(T eventData) where T : IEventData;
}