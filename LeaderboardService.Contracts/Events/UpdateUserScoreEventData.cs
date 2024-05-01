namespace LeaderboardService.Contracts.Events;

public record UpdateUserScoreEventData(int UserId, int Score, DateTime TimeStamp) :
    EventData(TimeStamp);