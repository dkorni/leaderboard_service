using LeaderboardService.Contracts;
using LeaderboardService.Domain;

namespace LeaderboardService.API.Extensions;

public static class LeaderboardMemberModelExtensions
{
    public static LeaderBoardMemberDto ToDto(this LeaderBoardMemberModel model) =>
        new LeaderBoardMemberDto
        {
            Id = model.Id,
            UserId = model.UserId,
            ScoreSummary = model.ScoreSummary,
            UpdatedAt = model.UpdatedAt
        };
}