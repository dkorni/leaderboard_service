namespace LeaderboardService.Contracts.Events;

public class UpdateUserScoreEventData : IEventData
{
    public int UserId { get; set; }

    public int Score { get; set; }
    
    public DateTime TimeStamp { get; set; }
}