using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using LeaderboardService.Domain.Constants;
using LeaderboardService.Domain.Exceptions;
using MediatR;

namespace LeaderboardService.Application.Queries;

public class GetAllLeaderBoardMembersHandler : IRequestHandler<GetAllLeaderBoardMembers, PagedResponse<LeaderBoardMemberModel>>
{
    private readonly IRepository<LeaderBoardMemberModel> _repository;

    public GetAllLeaderBoardMembersHandler(IRepository<LeaderBoardMemberModel> repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResponse<LeaderBoardMemberModel>> Handle(GetAllLeaderBoardMembers request, CancellationToken cancellationToken)
    {
        if (request.page <= 0)
            throw new ValidationException(ValidationExceptionMessages.InvalidPage);
        
        if (request.pageSize <= 0)
            throw new ValidationException(ValidationExceptionMessages.InvalidPageSize);
        
        var t1 = _repository.Count(_ => true);
        var t2 = _repository.GetAll(request.page, request.pageSize);

        await Task.WhenAll(t1, t2);

        var totalCount = t1.Result;
        var totalPages = (int)Math.Ceiling((decimal)totalCount / request.pageSize);

        var records = t2.Result;

        return new PagedResponse<LeaderBoardMemberModel>(request.page, request.pageSize, totalPages, totalCount, records);
    }
}