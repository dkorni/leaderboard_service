namespace LeaderboardService.Contracts.Events;

public record Event<T> (T EventData) where T : EventData;