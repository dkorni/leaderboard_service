using LeaderboardService.Contracts.Events;

namespace LeaderboardService.EventHandlers.Interfaces;

public interface IEventHandler<T> where T : EventData
{
    public Task Handle(T eventData);
}