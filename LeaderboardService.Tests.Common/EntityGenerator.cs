using LeaderboardService.Domain;
using LeaderboardService.Domain.Constants;

namespace LeaderboardService.Tests.Common;

public static class EntityGenerator
{
    public static LeaderBoardMemberModel GetLeaderBoardMemberModel()
    {
        var random = new Random();
        
        return new LeaderBoardMemberModel
        {
            Id = random.Next(Constraints.MinId, Constraints.MaxId),
            UserId = random.Next(Constraints.MinUserId, Constraints.MaxUserId),
            ScoreSummary = random.Next(Constraints.MinScore, Constraints.MaxScore),
            UpdatedAt = DateTime.UtcNow
        };
    }
}