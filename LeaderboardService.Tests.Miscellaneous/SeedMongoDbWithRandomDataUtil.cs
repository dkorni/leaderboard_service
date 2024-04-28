using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using LeaderboardService.MongoDB.DataProvider;
using LeaderboardService.Tests.Common;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardService.Tests.Miscellaneous;

public class SeedMongoDbWithRandomDataUtil
{
    [TestCase(10)]
    public async Task SeedMongoDbWithRandomData(int recordCount)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMongoDbDataProvider();
        var provider = serviceCollection.BuildServiceProvider();

        var repository = provider.GetRequiredService<IRepository<LeaderBoardMemberModel>>();

        for (int i = 0; i < recordCount; i++) await repository.AddOrUpdate(EntityGenerator.GetLeaderBoardMemberModel());
        
        Assert.Pass();
    }
}