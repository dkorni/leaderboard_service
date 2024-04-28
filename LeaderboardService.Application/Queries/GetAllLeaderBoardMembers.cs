using LeaderboardService.Domain;
using MediatR;

namespace LeaderboardService.Application.Queries;

public class GetAllLeaderBoardMembers : IRequest<LeaderBoardMemberModel[]>
{
}