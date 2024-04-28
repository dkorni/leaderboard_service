using LeaderboardService.Application.Queries;
using LeaderboardService.Domain;
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
    public async Task<LeaderBoardMemberModel[]> GetAll()
    {
        var getAllQuery = new GetAllLeaderBoardMembers();
        var results = await _mediator.Send(getAllQuery);
        return results;
    }
}