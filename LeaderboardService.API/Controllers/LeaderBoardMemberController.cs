using LeaderboardService.API.Extensions;
using LeaderboardService.Application.Queries;
using LeaderboardService.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderBoardMemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaderBoardMemberController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<PagedResponseDto<LeaderBoardMemberDto>> GetAll(int page = 1, int pageSize = 10)
    {
        var getAllQuery = new GetAllLeaderBoardMembers(page, pageSize);
        var response = await _mediator.Send(getAllQuery);


        return new PagedResponseDto<LeaderBoardMemberDto>(
            response.page,
            response.pageSize,
            response.totalPages,
            response.totalRecords,
            response.records.Select(x => x.ToDto()).ToArray());
    }
}