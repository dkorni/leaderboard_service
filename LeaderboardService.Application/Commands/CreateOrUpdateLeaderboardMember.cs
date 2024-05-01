using MediatR;

namespace LeaderboardService.Application.Commands;

public record CreateOrUpdateLeaderboardMember(int UserId, int Score, DateTime UpdatedAt)
    : IRequest;