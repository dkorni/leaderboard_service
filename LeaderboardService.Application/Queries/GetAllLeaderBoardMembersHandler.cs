using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using MediatR;

namespace LeaderboardService.Application.Queries;

public class GetAllLeaderBoardMembersHandler : IRequestHandler<GetAllLeaderBoardMembers, LeaderBoardMemberModel[]>
{
    private readonly IRepository<LeaderBoardMemberModel> _repository;

    public GetAllLeaderBoardMembersHandler(IRepository<LeaderBoardMemberModel> repository)
    {
        _repository = repository;
    }
    
    public Task<LeaderBoardMemberModel[]> Handle(GetAllLeaderBoardMembers request, CancellationToken cancellationToken)
    {
        return _repository.GetAll();
    }
}