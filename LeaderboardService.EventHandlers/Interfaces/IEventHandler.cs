using LeaderboardService.Contracts.Events;

namespace LeaderboardService.EventHandlers.Interfaces;

public interface IEventHandler<T> where T : IEventData
{
    public Task Handle(T eventData);
}