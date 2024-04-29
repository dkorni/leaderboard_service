namespace LeaderboardService.Contracts;

public class LeaderBoardMemberDto : EntityDto
{
    public int UserId { get; set; }

    public int ScoreSummary { get; set; }

    public DateTime UpdatedAt { get; set; }
}