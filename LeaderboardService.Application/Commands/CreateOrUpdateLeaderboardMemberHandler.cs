using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using MediatR;

namespace LeaderboardService.Application.Commands;

public class CreateOrUpdateLeaderboardMemberHandler : IRequestHandler<CreateOrUpdateLeaderboardMember>
{
    private readonly IRepository<LeaderBoardMemberModel> _repository;

    public CreateOrUpdateLeaderboardMemberHandler(IRepository<LeaderBoardMemberModel> repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(CreateOrUpdateLeaderboardMember request, CancellationToken cancellationToken)
    {
        var member = (await _repository.Find(x => x.UserId == request.UserId))
            .FirstOrDefault();

        if (member == null)
        {
            // we need to create member
            member = new LeaderBoardMemberModel()
            {
                UserId = request.UserId,
                ScoreSummary = request.Score,
                UpdatedAt = request.UpdatedAt
            };
        }

        await _repository.AddOrUpdate(member);
    }
}