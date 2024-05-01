using LeaderboardService.Application.Interfaces;
using LeaderboardService.Application.Queries;
using LeaderboardService.Domain;
using LeaderboardService.Domain.Constants;
using LeaderboardService.Domain.Exceptions;
using LeaderboardService.Tests.Common;
using Moq;

namespace LeaderboardService.Tests.Unit;

public class GetAllLeaderBoardMembersHandlerNegativeTests
{
    [Test]
    public void GetAllLeaderBoardMembersHandler_ShouldThrowValidationExceptionInvalidPage()
    {
        // arrange
        var page = 0;
        var pageSize = 12;
        
        var repository = new Mock<IRepository<LeaderBoardMemberModel>>();
        
        var handler = new GetAllLeaderBoardMembersHandler(repository.Object);
        var query = new GetAllLeaderBoardMembers(page, pageSize);

        // act
        var task = handler.Handle(query, new CancellationToken());

        // assert
        Assert.ThrowsAsync(Is.TypeOf<ValidationException>()
            .And.Message.EqualTo(ValidationExceptionMessages.InvalidPage), 
            () => task);
    }
}