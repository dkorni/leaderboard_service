using LeaderboardService.Domain;
using MediatR;

namespace LeaderboardService.Application.Queries;

public record GetAllLeaderBoardMembers(int page, int pageSize) : IRequest<PagedResponse<LeaderBoardMemberModel>>;
