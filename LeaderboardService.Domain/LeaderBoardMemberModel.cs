namespace LeaderboardService.Domain;

public class LeaderBoardMemberModel : Entity
{
    public int UserId { get; set; }

    public int ScoreSummary { get; set; }

    public DateTime UpdatedAt { get; set; }
}