using LeaderboardService.Contracts.Events;
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
            Id = Guid.NewGuid(),
            UserId = random.Next(Constraints.MinUserId, Constraints.MaxUserId),
            ScoreSummary = random.Next(Constraints.MinScore, Constraints.MaxScore),
            UpdatedAt = DateTime.UtcNow
        };
    }
    
    public static UpdateUserScoreEventData GetUpdateUserScoreEventData()
    {
        var random = new Random();

        var userId = random.Next(Constraints.MinUserId, Constraints.MaxUserId);
        var scoreSummary = random.Next(Constraints.MinScore, Constraints.MaxScore);
        var timeStamp = DateTime.UtcNow;

        return new UpdateUserScoreEventData()
        {
            UserId = userId,
            Score = scoreSummary,
            TimeStamp = timeStamp
        };
    }
}