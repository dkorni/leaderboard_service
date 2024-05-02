namespace LeaderboardService.Domain;

public class LeaderBoardMemberModel : IEntity
{
    public Guid Id { get; set; }

    public int UserId { get; set; }

    public int ScoreSummary { get; set; }

    public DateTime UpdatedAt { get; set; }
}