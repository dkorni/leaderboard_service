using LeaderboardService.Application.Interfaces;
using LeaderboardService.Application.Queries;
using LeaderboardService.Domain;
using LeaderboardService.Tests.Common;
using Moq;

namespace LeaderboardService.Tests.Unit;

public class GetAllLeaderBoardMembersHandlerTests
{
    [Test]
    public async Task GetAllLeaderBoardMembersHandler_ShouldRetrieveRecords()
    {
        // arrange
        var recordsCount = 15;
        var page = 1;
        var pageSize = 12;
        var totalPages = 2;
        var records = new List<LeaderBoardMemberModel>();
        var repository = new Mock<IRepository<LeaderBoardMemberModel>>();

        for(int i = 0; i < recordsCount; i++) records.Add(EntityGenerator.GetLeaderBoardMemberModel());
        var expectedRecords = records.Take(pageSize).ToArray().ToArray();
        
        repository
            .Setup(x => x.Count(x => true))
            .ReturnsAsync(recordsCount);
        
        repository
            .Setup(x => x.GetAll(page, pageSize))
            .ReturnsAsync(expectedRecords);
        
        var handler = new GetAllLeaderBoardMembersHandler(repository.Object);
        var query = new GetAllLeaderBoardMembers(page, pageSize);

        // act
        var response = await handler.Handle(query, new CancellationToken());

        // assert
        Assert.That(response.page, Is.EqualTo(page));
        Assert.That(response.pageSize, Is.EqualTo(pageSize));
        Assert.That(response.totalPages, Is.EqualTo(totalPages));
        Assert.That(response.totalRecords, Is.EqualTo(recordsCount));
        Assert.That(response.records, Is.EqualTo(expectedRecords));
    }
}