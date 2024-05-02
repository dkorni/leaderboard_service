using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using MediatR;
using Serilog;
using Serilog.Context;

namespace LeaderboardService.Application.Commands;

public class CreateOrUpdateLeaderboardMemberHandler : IRequestHandler<CreateOrUpdateLeaderboardMember>
{
    private readonly IRepository<LeaderBoardMemberModel> _repository;
    private readonly ILogger _logger;

    public CreateOrUpdateLeaderboardMemberHandler(IRepository<LeaderBoardMemberModel> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task Handle(CreateOrUpdateLeaderboardMember request, CancellationToken cancellationToken)
    {
        var member = (await _repository.Find(x => x.UserId == request.UserId))
            .FirstOrDefault();

        LogContext.PushProperty("Member", member);
        
        if (member == null)
        {
            // we need to create member
            member = new LeaderBoardMemberModel()
            {
                UserId = request.UserId,
                ScoreSummary = request.Score,
                UpdatedAt = request.UpdatedAt
            };
            
            _logger.Information("Creating new leaderboard member with UserId: {UserId}", member.UserId);
        }
        else
        {
            _logger.Information("Updating new leaderboard member with UserId: {UserId}", member.UserId);
        }

        await _repository.AddOrUpdate(member);
    }
}