namespace LeaderboardService.Contracts.Events;

public class Event<T> : IEvent<T> where T : IEventData
{
     public T EventData { get; }

     public Event(T eventData)
     {
          EventData = eventData;
     }
}

public interface IEvent<out T> where T : IEventData
{
     T EventData { get; }
}